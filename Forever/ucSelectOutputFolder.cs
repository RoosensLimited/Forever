namespace Forever
{
    public partial class ucSelectOutputFolder : ucTemplate
    {
        public ucSelectOutputFolder()
        {
            SetNextButtonText("Create");

            InitializeComponent();

            ApplyDarkModeToControls(this);

            if (!string.IsNullOrEmpty(ForeverSettings.OutputDirectory))
            {
                tbOutputFolder.Text = ForeverSettings.OutputDirectory;
            }
            else
            {
                tbOutputFolder.Text = Path.Combine(Path.Combine(Path.GetDirectoryName(Helpers.GetExecutablePath()), "Forever"), GetLastFolderName(ForeverSettings.InstallationDirectory));
            }

            if (Directory.Exists(tbOutputFolder.Text) && !Helpers.IsDirectoryEmpty(tbOutputFolder.Text))
            {
                tbOutputFolder.Text = string.Empty;
            }

            cbSplit.SelectedIndex = (int)ForeverSettings.PartSize;
            
            tbOutputFolder.TextChanged += control_Changed;
            cbSplit.SelectedIndexChanged += control_Changed;
        }

        private void control_Changed(object? sender, EventArgs e)
        {
            ValidateForm();
        }

        internal override void ValidateForm()
        {
            ForeverSettings.OutputDirectory = tbOutputFolder.Text;
            ForeverSettings.PartSize = (ArchivePartSize)cbSplit.SelectedIndex;
            SetNextButtonEnabled(!string.IsNullOrEmpty(ForeverSettings.OutputDirectory) && Helpers.IsValidFolderPath(ForeverSettings.OutputDirectory));
        }

        public static string GetLastFolderName(string fullPath)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
                return string.Empty;

            // This is the simplest and most reliable way
            return Path.GetFileName(Path.TrimEndingDirectorySeparator(fullPath));
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog()
            {
                Description = "Select the folder where you want to save the installer. For example D:\\Backups\\GameName",
                ShowNewFolderButton = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            while (dialog.ShowDialog() == DialogResult.OK && Directory.Exists(dialog.SelectedPath))
            {
                try
                {

                    if (!Helpers.IsDirectoryEmpty(dialog.SelectedPath))
                    {
                        if (MessageBox.Show("The output directory needs to be empty", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            continue;
                        }

                        return;
                    }
                }
                catch
                {
                    return;
                }

                tbOutputFolder.Text = dialog.SelectedPath;
                break;
            }
        }
    }
}
