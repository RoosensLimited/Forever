using System.ComponentModel;
using System.Data;
using System.Text;

namespace Forever
{
    public partial class ucInstallerRoosensDecryptionKey : ucTemplateInstaller
    {
        public ucInstallerRoosensDecryptionKey()
        {
            InitializeComponent();

            ApplyDarkModeToControls(this);

            if (string.IsNullOrEmpty(ForeverInstallerSettings.RoosensDecryptionKey))
            {
                var decryptionKeyFile = Path.Combine(ForeverInstallerSettings.ArchiveDirectory, "decryptionkey.txt");
                if (File.Exists(decryptionKeyFile))
                {
                    try
                    {
                        tbRoosensDecryptionKey.Text = File.ReadAllText(decryptionKeyFile).Split("\r\n".ToCharArray()).First().Trim();
                    }
                    catch
                    { }
                }
            }
            else
            {
                tbRoosensDecryptionKey.Text = ForeverInstallerSettings.RoosensDecryptionKey;
            }

            SetNextButtonText("&Install");

            ValidateForm();

            tbRoosensDecryptionKey.TextChanged += control_Changed;
            tbRoosensDecryptionKey.Focus();
        }

        private void control_Changed(object? sender, EventArgs e)
        {
            ValidateForm();
        }

        internal override void ValidateForm()
        {
            ForeverInstallerSettings.RoosensDecryptionKey = tbRoosensDecryptionKey.Text;

            if (ForeverInstallerSettings.RoosensDecryptionKey.Length > 0 && ForeverInstallerSettings.RoosensDecryptionKey.Length != 128)
            {
                SetNextButtonEnabled(false);
                return;
            }

            SetNextButtonEnabled(true);
            return;
        }
    }
}
