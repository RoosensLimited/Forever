using Ionic.Zip;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;

namespace Forever
{
    public partial class ucInstallerInstallation : ucTemplateInstaller
    {
        public ucInstallerInstallation()
        {
            InitializeComponent();

            SetNextButtonText("&Close");
            SetNextButtonEnabled(false);

            pbInstalling.Image = global::Forever.Properties.Resources.loading;

            bwInstalling.RunWorkerAsync();

            VisibleChanged += UcInstallationBusy_VisibleChanged;
        }

        private void UcInstallationBusy_VisibleChanged(object? sender, EventArgs e)
        {
            if (!Visible)
            {
                if (bwInstalling != null && bwInstalling.IsBusy)
                {
                    try
                    {
                        bwInstalling.CancelAsync();
                    }
                    catch { }
                }

                if (bwFinalizing != null && bwFinalizing.IsBusy)
                {
                    try
                    {
                        bwFinalizing.CancelAsync();
                    }
                    catch { }
                }
            }
        }

        internal override void ValidateForm()
        {
        }

        public static bool RunSteamInstallScript(string vdfPath, string installDir)
        {
            if (!File.Exists(vdfPath))
            {
                return false;
            }

            var vdfContent = File.ReadAllText(vdfPath);

            var vdf = VdfParser.Parse(vdfContent);

            if (vdf.ContainsKey("InstallScript"))
            {
                var vdfInstallScript = (Dictionary<string, object>)vdf["InstallScript"];
                if (vdfInstallScript.ContainsKey("Run Process"))
                {
                    var vdfRunProcess = (Dictionary<string, object>)vdfInstallScript["Run Process"];

                    foreach (Dictionary<string, object> app in vdfRunProcess.Values)
                    {
                        int processId = 1;

                        while (app.ContainsKey($"process {processId}"))
                        {
                            string path = (string)app[$"process {processId}"];
                            path = path.Replace("%INSTALLDIR%", installDir);

                            string command = string.Empty;
                            if (app.ContainsKey($"command {processId}"))
                            {
                                command = (string)app[$"command {processId}"];
                            }

                            Helpers.RunProcess(path, command);

                            processId++;
                        }
                    }
                }
            }

            return true;
        }

        private void bwInstalling_DoWork(object sender, DoWorkEventArgs e)
        {
            ForeverInstallerSettings.MainForm.Busy = true;

            try
            {
                if (!Directory.Exists(ForeverInstallerSettings.InstallationDirectory))
                {
                    Directory.CreateDirectory(ForeverInstallerSettings.InstallationDirectory);
                }

                //extract game
                using (ZipFile zip = ZipFile.Read(Path.Combine(ForeverInstallerSettings.ArchiveDirectory, "game.zip")))
                {
                    zip.ExtractProgress += (s, zipEvent) =>
                    {
                        if (bwInstalling.CancellationPending)
                        {
                            zipEvent.Cancel = true;
                            return;
                        }
                    };

                    zip.ExtractAll(ForeverInstallerSettings.InstallationDirectory, ExtractExistingFileAction.OverwriteSilently);
                }

                //extract redist
                var redistArchivePath = Path.Combine(ForeverInstallerSettings.ArchiveDirectory, "redist.zip");
                if (File.Exists(redistArchivePath))
                {
                    var redistDirectory = Path.Combine(ForeverInstallerSettings.InstallationDirectory, ForeverSettings.InstallationDirectory + "-redist");

                    if (!Directory.Exists(redistDirectory))
                    {
                        Directory.CreateDirectory(redistDirectory);
                    }

                    using (ZipFile zip = ZipFile.Read(redistArchivePath))
                    {
                        zip.ExtractProgress += (s, zipEvent) =>
                        {
                            if (bwInstalling.CancellationPending)
                            {
                                zipEvent.Cancel = true;
                                return;
                            }
                        };

                        zip.ExtractAll(redistDirectory, ExtractExistingFileAction.OverwriteSilently);
                    }
                }

                //perform scan
                var allExecutablePaths = Directory.EnumerateFiles(
                    ForeverInstallerSettings.InstallationDirectory,
                    "*.*",
                    SearchOption.AllDirectories)
                .Where(f =>
                    f.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) ||
                    f.EndsWith(".dll", StringComparison.OrdinalIgnoreCase));

                var allExecutablePathsSorted = allExecutablePaths.ToList();
                allExecutablePathsSorted.Sort();

                var unsignedExecutablePaths = new List<string>();

                foreach (var executablPath in allExecutablePathsSorted)
                {
                    if (SignatureVerifier.VerifySignature(executablPath) != SignatureVerifier.SignatureCheckResult.ValidSigned)
                    {
                        unsignedExecutablePaths.Add(executablPath.Substring(ForeverInstallerSettings.InstallationDirectory.Length));
                    }
                }

                if (unsignedExecutablePaths.Count > 0)
                {
                    e.Result = unsignedExecutablePaths;
                }
            }
            catch (Exception ex)
            {
                e.Result = "Installation failed: " + ex.Message;
            }
        }

        private void bwInstalling_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null && e.Result is string)
            {
                pbInstalling.Visible = false;
                ForeverInstallerSettings.MainForm.SetTitle((string)e.Result, false);
                ForeverInstallerSettings.MainForm.Busy = false;
                return;
            }

            if (e.Result != null && e.Result is List<string>)
            {
                var unsignedExecutablePaths = e.Result as List<string>;

                if (unsignedExecutablePaths.Count > 0)
                {
                    pbInstalling.Visible = false;
                    if (new ForeverUnsignedFiles(unsignedExecutablePaths).ShowDialog(this) != DialogResult.Continue)
                    {
                        bwFinalizing.RunWorkerAsync(false);
                    }
                    else
                    {
                        bwFinalizing.RunWorkerAsync(true);
                    }
                    pbInstalling.Visible = true;
                }
                return;
            }
            
            bwFinalizing.RunWorkerAsync(true);
        }

        private void bwFinalizing_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bool continueInstallation = (bool)e.Argument;

                if (!continueInstallation)
                {
                    try
                    {
                        Directory.Delete(ForeverInstallerSettings.InstallationDirectory, true);
                    }
                    catch { }

                    e.Result = "Installation aborted";
                    return;
                }

                //remove roosens
                if (!string.IsNullOrEmpty(ForeverInstallerSettings.RoosensDecryptionKey))
                {
                    var executablePaths = Helpers.FindExeAndDllFiles(ForeverInstallerSettings.InstallationDirectory);

                    foreach (var executablePath in executablePaths)
                    {
                        if (bwInstalling.CancellationPending)
                        {
                            return;
                        }

                        if (RemoveProtection.HasRoosensSection(executablePath))
                        {
                            if (!RemoveProtection.RemoveProtectionFromFile(executablePath, ForeverInstallerSettings.RoosensDecryptionKey))
                            {
                                e.Result = $"Could not remove Roosens protection from '{executablePath}'. Your decryption key is invalid or the protection was already removed.";
                                return;
                            }
                        }
                    }
                }

                //run redist
                var redistArchivePath = Path.Combine(ForeverInstallerSettings.ArchiveDirectory, "redist.zip");
                if (File.Exists(redistArchivePath))
                {
                    var redistDirectory = Path.Combine(ForeverInstallerSettings.InstallationDirectory, ForeverSettings.InstallationDirectory + "-redist");

                    var redistExecutables = Directory.EnumerateFiles(redistDirectory, "*.exe", SearchOption.AllDirectories);
                    var executedInstallScripts = new List<string>();
                    foreach (var redist in redistExecutables)
                    {
                        if (bwInstalling.CancellationPending)
                        {
                            return;
                        }

                        var installScripts = Directory.EnumerateFiles(Path.GetDirectoryName(redist), "*.vdf", SearchOption.TopDirectoryOnly);
                        if (installScripts.Count() != 0)
                        {
                            foreach (var vdfPath in installScripts)
                            {
                                if (executedInstallScripts.Contains(vdfPath))
                                {
                                    continue;
                                }

                                RunSteamInstallScript(vdfPath, redistDirectory);
                                executedInstallScripts.Add(vdfPath);
                            }
                        }
                        else
                        {
                            int exitCode;
                            if (redist.ToLower().EndsWith("dxsetup.exe"))
                            {
                                exitCode = Helpers.RunProcess(redist, "/silent");
                            }
                            else
                            {
                                exitCode = Helpers.RunProcess(redist, string.Empty);
                            }
                        }

                        if (bwInstalling.CancellationPending)
                        {
                            return;
                        }
                    }
                }

                if (ForeverInstallerSettings.CreateDesktopShortcut)
                {
                    var shortcutTargetPath = Path.Combine(ForeverInstallerSettings.InstallationDirectory, ForeverSettings.GameLaunchExecutablePath);
                    AppInstaller.CreateDesktopShortcut(ForeverSettings.GameName, shortcutTargetPath);
                }

                if (ForeverInstallerSettings.CreateStartMenuShortcut)
                {
                    var shortcutTargetPath = Path.Combine(ForeverInstallerSettings.InstallationDirectory, ForeverSettings.GameLaunchExecutablePath);
                    AppInstaller.CreateStartMenuShortcut(ForeverSettings.GameName, shortcutTargetPath);
                }

                if (ForeverInstallerSettings.CreateUninstaller)
                {
                    var unintallExePath = Path.Combine(ForeverInstallerSettings.InstallationDirectory, "uninstall.exe");

                    string appName = ForeverSettings.InstallationDirectory + "_Forever";
                    System.IO.File.WriteAllText(Path.Combine(ForeverInstallerSettings.InstallationDirectory, "uninstall.txt"), $"{appName}\r\n{ForeverSettings.GameName}");

                    File.Copy(Helpers.GetExecutablePath(), unintallExePath);

                    AppInstaller.RegisterForUninstall(
                        appName: appName,
                        displayName: ForeverSettings.GameName,
                        publisher: "Forever",
                        version: "1.0.0",
                        uninstallExePath: unintallExePath,
                        installLocation: ForeverInstallerSettings.InstallationDirectory
                    );
                }
            }
            catch (Exception ex)
            {
                e.Result = "Installation failed: " + ex.Message;
            }
        }

        private void bwFinalizing_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ForeverInstallerSettings.MainForm.Busy = false;

            pbInstalling.Visible = false;
            SetNextButtonEnabled(true);

            if (e.Result != null && e.Result is string)
            {
                ForeverInstallerSettings.MainForm.SetTitle((string)e.Result, false);
                return;
            }

            ForeverInstallerSettings.MainForm.SetTitle("Installation complete!");
        }
    }
}
