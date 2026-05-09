namespace Forever
{
    public partial class ucInstallerSettings : ucTemplateInstaller
    {
        public ucInstallerSettings()
        {
            InitializeComponent();

            ApplyDarkModeToControls(this);

            if (ForeverSettings.RoosensExecutablesDetected)
            {
                SetNextButtonText("&Next");
            }
            else
            {
                SetNextButtonText("&Install");
            }

            tbGameInstallationFolder.Text = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Forever"), ForeverSettings.InstallationDirectory);

            if (Directory.Exists(tbGameInstallationFolder.Text) && !Helpers.IsDirectoryEmpty(tbGameInstallationFolder.Text))
            {
                tbGameInstallationFolder.Text = string.Empty;
            }

            cbCreateDesktopShortcut.Checked = ForeverInstallerSettings.CreateDesktopShortcut;
            cbCreateStartmenuShortcut.Checked = ForeverInstallerSettings.CreateStartMenuShortcut;
            cbCreateUninstaller.Checked = ForeverInstallerSettings.CreateUninstaller;

            ValidateForm();

            tbGameInstallationFolder.TextChanged += control_Changed;
            cbCreateDesktopShortcut.CheckedChanged += control_Changed;
            cbCreateStartmenuShortcut.CheckedChanged += control_Changed;
            cbCreateUninstaller.CheckedChanged += control_Changed;

            SetNextButtonFocus();
        }

        private void control_Changed(object? sender, EventArgs e)
        {
            ValidateForm();
        }

        internal override void ValidateForm()
        {
            ForeverInstallerSettings.InstallationDirectory = tbGameInstallationFolder.Text;

            ForeverInstallerSettings.CreateDesktopShortcut = cbCreateDesktopShortcut.Checked;
            ForeverInstallerSettings.CreateStartMenuShortcut = cbCreateStartmenuShortcut.Checked;
            ForeverInstallerSettings.CreateUninstaller = cbCreateUninstaller.Checked;

            SetNextButtonEnabled(!string.IsNullOrEmpty(ForeverInstallerSettings.InstallationDirectory) && Helpers.IsValidFolderPath(ForeverInstallerSettings.InstallationDirectory));
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog()
            {
                Description = "Select the game installation folder. For example C:\\Games\\GameName",
                ShowNewFolderButton = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            };

            while (dialog.ShowDialog() == DialogResult.OK && Directory.Exists(dialog.SelectedPath))
            {
                try
                {

                    if (!Helpers.IsDirectoryEmpty(dialog.SelectedPath))
                    {
                        tbGameInstallationFolder.Text = Path.Combine(dialog.SelectedPath, ForeverSettings.InstallationDirectory);
                        break;
                    }
                }
                catch
                {
                    return;
                }

                tbGameInstallationFolder.Text = dialog.SelectedPath;
                break;
            }

            ValidateForm();
        }
    }
}
