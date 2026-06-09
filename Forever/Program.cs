namespace Forever
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            Application.SetColorMode(SystemColorMode.Dark);

            string exePath = Helpers.GetExecutablePath();

            string currentFolder = Path.GetDirectoryName(exePath);
            string uninstallTxtPath = Path.Combine(currentFolder, "uninstall.txt");

            if (File.Exists(uninstallTxtPath))
            {
                Helpers.RestartAsAdmin();

                string appName = null;
                string displayName = null;

                try
                {
                    var lines = File.ReadAllLines(uninstallTxtPath);
                    if(lines.Length == 2)
                    {
                        appName = lines[0];
                        displayName = lines[1];
                    }
                }
                catch
                {
                }

                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(displayName))
                {
                    if(!args.Contains("--quiet"))
                    {
                        if (MessageBox.Show($"Uninstall {displayName}?", "Forever", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            return;
                        }
                    }

                    AppUninstaller.PerformUninstall(appName, displayName, currentFolder);
                }

                return;
            }

            if (exePath.ToLower().EndsWith("setup.exe"))
            {
                ForeverInstallerSettings.ArchiveDirectory = Path.GetDirectoryName(exePath);

                var gameArchivePath = Path.Combine(ForeverInstallerSettings.ArchiveDirectory, "game.zip");
                var settingsArchivePath = Path.Combine(ForeverInstallerSettings.ArchiveDirectory, "setup.json");

                if (File.Exists(gameArchivePath) && File.Exists(settingsArchivePath))
                {
                    if (ForeverSettings.LoadJson(settingsArchivePath))
                    {
#if !DEBUG
                        Helpers.RestartAsAdmin();
#endif

                        Application.Run(new ForeverInstaller());
                        return;
                    }
                }
            }

            Application.Run(new Forever());
        }
    }
}