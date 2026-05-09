using Microsoft.Win32;

namespace Forever
{
    public partial class ucSelectGameFolder : ucTemplate
    {
        private Dictionary<string, string> SteamFolderNames;
        public ucSelectGameFolder()
        {
            InitializeComponent();

            SetNextButtonText("&Next");

            ApplyDarkModeToControls(this);

            InitializeControls();

            rbSteam.CheckedChanged += control_Changed;
            rbCustomFolder.CheckedChanged += control_Changed;
            cbSteamGames.SelectedIndexChanged += control_Changed;
            tbCustomFolderGameName.TextChanged += control_Changed;
        }

        private void control_Changed(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void InitializeControls()
        {
            SteamFolderNames = FindSteamGames();

            rbSteam.Enabled = (SteamFolderNames != null && SteamFolderNames.Count > 0);
            cbSteamGames.Enabled = rbSteam.Enabled;


            if (SteamFolderNames != null && SteamFolderNames.Count > 0)
            {
                var SteamGameItems = new List<string>();
                foreach (var SteamFolderName in SteamFolderNames)
                {
                    SteamGameItems.Add(SteamGameToString(SteamFolderName));
                }

                SteamGameItems.Sort();
                cbSteamGames.Items.AddRange(SteamGameItems.ToArray());

                if (ForeverSettings.IsSteamGame && !string.IsNullOrEmpty(ForeverSettings.InstallationDirectory) && !(string.IsNullOrEmpty(ForeverSettings.GameName)))
                {
                    rbSteam.Checked = true;
                    cbSteamGames.SelectedIndex = SteamGameItems.IndexOf($"{ForeverSettings.GameName} ({ForeverSettings.InstallationDirectory})");
                }
                else
                {
                    cbSteamGames.SelectedIndex = 0;
                }
            }
            else
            {
                rbCustomFolder.Checked = true;
            }

            if (!ForeverSettings.IsSteamGame && !string.IsNullOrEmpty(ForeverSettings.InstallationDirectory) && !(string.IsNullOrEmpty(ForeverSettings.GameName)))
            {
                tbCustomFolder.Text = ForeverSettings.InstallationDirectory;
                tbCustomFolderGameName.Text = ForeverSettings.GameName;

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

            string steamPath = GetSteamPath();
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
                    if (!gameName.ToLower().Contains("steamworks"))
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

        private void bBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog()
            {
                Description = "Select the root folder of the game you want to preserve. For example C:\\Games\\GameName",
                ShowNewFolderButton = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (dialog.ShowDialog() != DialogResult.OK || !Directory.Exists(dialog.SelectedPath))
            {
                return;
            }

            rbCustomFolder.Checked = true;
            tbCustomFolder.Text = dialog.SelectedPath;

            tbCustomFolderGameName.Text = tbCustomFolder.Text.Split('\\', StringSplitOptions.RemoveEmptyEntries).Last();

            ValidateForm();
        }

        internal override void ValidateForm()
        {
            if (rbCustomFolder.Checked)
            {
                ForeverSettings.IsSteamGame = false;

                if (string.IsNullOrEmpty(tbCustomFolder.Text))
                {
                    ForeverSettings.InstallationDirectory = null;
                    ForeverSettings.GameName = null;
                }
                else
                {
                    ForeverSettings.InstallationDirectory = tbCustomFolder.Text;
                    ForeverSettings.GameName = tbCustomFolderGameName.Text;
                }
            }
            else
            {
                ForeverSettings.IsSteamGame = true;

                var selectedText = cbSteamGames.Text;

                foreach (var SteamFolderName in SteamFolderNames)
                {
                    if (SteamGameToString(SteamFolderName) == selectedText)
                    {
                        ForeverSettings.InstallationDirectory = SteamFolderName.Key;
                        ForeverSettings.GameName = SteamFolderName.Value;
                        break;
                    }
                }
            }

            SetNextButtonEnabled(!string.IsNullOrEmpty(ForeverSettings.GameName) && !string.IsNullOrEmpty(ForeverSettings.InstallationDirectory));
        }

        private void tbCustomFolder_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
