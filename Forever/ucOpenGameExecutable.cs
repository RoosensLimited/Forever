namespace Forever
{
    public partial class ucOpenGameExecutable : ucTemplate
    {
        public ucOpenGameExecutable()
        {
            InitializeComponent();

            ApplyDarkModeToControls(this);

            if (ForeverSettings.GameLaunchExecutablePath != null && !ForeverSettings.GameLaunchExecutablePath.ToLower().Contains(ForeverSettings.InstallationDirectory.ToLower()))
            {
                ForeverSettings.GameLaunchExecutablePath = null;
            }

            if (!string.IsNullOrEmpty(ForeverSettings.GameLaunchExecutablePath))
            {
                tbGameExecutable.Text = ForeverSettings.GameLaunchExecutablePath;
            }
            else
            {
                try
                {
                    var files = Directory.GetFiles(ForeverSettings.InstallationDirectory, "*.exe").ToList();
                    files = files.FindAll(a => !a.ToLower().StartsWith("unitycrashhandler"));
                    if (files.Count == 1)
                    {
                        tbGameExecutable.Text = files[0];
                    }
                }
                catch { }
            }
            
            tbGameExecutable.TextChanged += control_Changed;
        }

        private void control_Changed(object? sender, EventArgs e)
        {
            ValidateForm();
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Open game launch executable";

            ofd.Filter = "Executable (*.exe)|*.exe";
            
            ofd.InitialDirectory = ForeverSettings.InstallationDirectory;
            ofd.RestoreDirectory = true;


            while (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!ofd.FileName.ToLower().Contains(ForeverSettings.InstallationDirectory.ToLower()))
                {
                    if (MessageBox.Show("Your game executable must be in your game installation directory", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }

                tbGameExecutable.Text = ofd.FileName;

                ValidateForm();
                return;
            }

            ValidateForm();
        }

        internal override void ValidateForm()
        {
            ForeverSettings.GameLaunchExecutablePath = tbGameExecutable.Text;

            SetNextButtonEnabled(!string.IsNullOrEmpty(ForeverSettings.GameLaunchExecutablePath));
        }
    }
}
