using Microsoft.Win32;

namespace Forever
{
    public partial class ucSelectRedistributables : ucTemplate
    {
        private Dictionary<string, string> SteamFolderNames;
        public ucSelectRedistributables()
        {
            SetNextButtonText("&Next");

            InitializeComponent();

            pictureBox1.Image = global::Forever.Properties.Resources.information;

            ApplyDarkModeToControls(this);

            InitializeControls();

            rbSteam.CheckedChanged += control_Changed;
            rbCustomFolder.CheckedChanged += control_Changed;
            cbSteamRedistributables.SelectedIndexChanged += control_Changed;
        }

        private void control_Changed(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void InitializeControls()
        {
            SteamFolderNames = FindSteamGames();

            rbSteam.Enabled = (SteamFolderNames != null && SteamFolderNames.Count > 0);
            cbSteamRedistributables.Enabled = rbSteam.Enabled;


            if (SteamFolderNames != null && SteamFolderNames.Count > 0)
            {
                var SteamGameItems = new List<string>();
                foreach (var SteamFolderName in SteamFolderNames)
                {
                    SteamGameItems.Add(SteamGameToString(SteamFolderName));
                }

                SteamGameItems.Sort();
                cbSteamRedistributables.Items.AddRange(SteamGameItems.ToArray());

                if(ForeverSettings.IsSteamRedistributables && !string.IsNullOrEmpty(ForeverSettings.RedistributablesDirectory))
                {
                    rbSteam.Checked = true;

                    int index = 0;
                    for (int i = 0; i < SteamGameItems.Count; i++)
                    {
                        if (SteamGameItems[i].Contains($"({ForeverSettings.RedistributablesDirectory})"))
                        {
                            cbSteamRedistributables.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    cbSteamRedistributables.SelectedIndex = 0;
                }
            }
            else
            {
                rbCustomFolder.Checked = true;
            }

            if (!ForeverSettings.IsSteamRedistributables && !string.IsNullOrEmpty(ForeverSettings.RedistributablesDirectory))
            {
                tbCustomFolder.Text = ForeverSettings.RedistributablesDirectory;

                rbCustomFolder.Checked = true;
            }
            else
            {
                rbSteam.Checked = true;
            }

                ValidateForm();
        }

        private static string SteamGameToString(KeyValuePair<string, string> steamGame)
        {
            return $"{steamGame.Value} ({steamGame.Key})";
        }

        private static string GetSteamPath()
        {
            // Try 64-bit first
            using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Valve\Steam"))
            {
                if (key?.GetValue("InstallPath") is string path && !string.IsNullOrEmpty(path))
                {
                    return path;
                }
            }

            // Fallback to 32-bit
            using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Valve\Steam"))
            {
                if (key?.GetValue("InstallPath") is string path && !string.IsNullOrEmpty(path))
                {
                    return path;
                }
            }

            return null;
        }

        private static Dictionary<string, string> FindSteamGames()
        {
            List<string> steamAppsPaths = null;

            var steamPath = GetSteamPath();
            if (!string.IsNullOrEmpty(steamPath))
            {
                steamAppsPaths = VdfParser.GetSteamAppsFolders(Path.Combine(steamPath, "steamapps\\libraryfolders.vdf"));
            }

            if (steamAppsPaths == null || steamAppsPaths.Count == 0)
            {
                return null;
            }

            var acfFilePaths = new List<string>();

            foreach (string steamAppsPath in steamAppsPaths)
            {
                try
                {
                    if (Directory.Exists(steamAppsPath))
                    {
                        try
                        {
                            var paths = Directory.GetFiles(steamAppsPath, "*.acf");
                            if (paths != null)
                            {
                                acfFilePaths.AddRange(paths);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }
            }

            if (acfFilePaths.Count == 0)
            {
                return null;
            }

            var steamGames = new Dictionary<string, string>();

            foreach (var acfFilePath in acfFilePaths)
            {
                string installDir = null;
                string gameName = null;

                if (VdfParser.GetAcfData(acfFilePath, out installDir, out gameName))
                {
                    if (gameName.ToLower().Contains("steamworks"))
                    {
                        steamGames.Add(installDir, gameName);
                    }
                }
            }

            if (steamGames.Count == 0)
            {
                return null;
            }

            return steamGames;
        }

        public static bool ContainsExecutables(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
                return false;

            try
            {
                return Directory.EnumerateFiles(folderPath, "*.exe", SearchOption.AllDirectories)
                                .Any();
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog()
            {
                Description = "Select the root folder of the redistributables. For example C:\\Games\\GameName",
                SelectedPath = @"C:\",
                ShowNewFolderButton = false
            };

            while (dialog.ShowDialog() == DialogResult.OK && Directory.Exists(dialog.SelectedPath))
            {
                if (!ContainsExecutables(dialog.SelectedPath))
                {
                    if (MessageBox.Show("Could not find any executables in the selected folder", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }

                rbCustomFolder.Checked = true;
                tbCustomFolder.Text = dialog.SelectedPath;

                ValidateForm();
                return;
            }
        }

        internal override void ValidateForm()
        {
            if (rbCustomFolder.Checked)
            {
                ForeverSettings.IsSteamRedistributables = false;

                if (string.IsNullOrEmpty(tbCustomFolder.Text))
                {
                    ForeverSettings.RedistributablesDirectory = null;
                }
                else
                {
                    ForeverSettings.RedistributablesDirectory = tbCustomFolder.Text;
                }
            }
            else
            {
                ForeverSettings.IsSteamRedistributables = true;

                string selectedText = cbSteamRedistributables.Text;

                foreach (var SteamFolderName in SteamFolderNames)
                {
                    if (selectedText.Contains($" ({SteamFolderName.Key})"))
                    {
                        ForeverSettings.RedistributablesDirectory = SteamFolderName.Key;
                        break;
                    }
                }
            }

            SetNextButtonEnabled(!string.IsNullOrEmpty(ForeverSettings.RedistributablesDirectory));
        }
    }
}
