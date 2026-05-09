using System.ComponentModel;
using System.Data;
using System.Text;

namespace Forever
{
    public partial class ucRoosensDecryptionKey : ucTemplate
    {
        public ucRoosensDecryptionKey()
        {
            InitializeComponent();

            pbFindRoosensExecutables.Image = global::Forever.Properties.Resources.loading;

            ApplyDarkModeToControls(this);

            SetNextButtonEnabled(false);

            bwFindRoosensExecutable.RunWorkerAsync();

            tbRoosensDecryptionKey.TextChanged += control_Changed;
        }

        private void control_Changed(object? sender, EventArgs e)
        {
            ValidateForm();
        }

        internal override void ValidateForm()
        {
            if (bwFindRoosensExecutable.IsBusy)
            {
                return;
            }

            if (tbRoosensDecryptionKey.Visible)
            {
                ForeverSettings.RoosensDecryptionKey = tbRoosensDecryptionKey.Text;
            }
            else
            {
                ForeverSettings.RoosensDecryptionKey = null;
            }

            if (tbRoosensDecryptionKey.Visible)
            {
                if (tbRoosensDecryptionKey.Text.Length > 0 && tbRoosensDecryptionKey.Text.Length != 128)
                {
                    SetNextButtonText("&Next");
                    SetNextButtonEnabled(false);
                    return;
                }

                if (tbRoosensDecryptionKey.Text.Length == 0)
                {
                    SetNextButtonText("&Skip");
                }
                else
                {
                    SetNextButtonText("&Next");
                }

                SetNextButtonEnabled(true);
                return;
            }

            SetNextButtonEnabled(true);
        }

        

        

        private void bwFindRoosensExecutable_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);

            var executablePaths = Helpers.FindExeAndDllFiles(ForeverSettings.InstallationDirectory);

            if (bwFindRoosensExecutable.CancellationPending)
            {
                return;
            }

            foreach (var executablePath in executablePaths)
            {
                if (RemoveProtection.HasRoosensSection(executablePath))
                {
                    e.Result = true;
                    return;
                }

                if (bwFindRoosensExecutable.CancellationPending)
                {
                    return;
                }
            }

            e.Result = false;
        }

        private void bwFindRoosensExecutable_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbFindRoosensExecutables.Visible = false;

            if (e.Result != null && e.Result is bool && (bool)e.Result == true)
            {
                tbRoosensDecryptionKey.Visible = true;
                lRoosensInfo.Text = "This game includes at least one Roosens protected executable.\r\nEither enter a Roosens decryption key now, or make a decryptionkey.txt later and save it in the same folder as the generated installer.";
                lRoosensDecyptionKey.Text = "Roosens Decryption Key";
                tbRoosensDecryptionKey.Text = ForeverSettings.RoosensDecryptionKey;

                tbRoosensDecryptionKey.TextChanged += control_Changed;
                ForeverSettings.RoosensExecutablesDetected = true;
                tbRoosensDecryptionKey.Focus();
                ValidateForm();
                return;
            }

            ForeverSettings.RoosensExecutablesDetected = false;
            tbRoosensDecryptionKey.Visible = false;
            lRoosensDecyptionKey.Text = "This game does not contain Roosens protected executables.";
            tbRoosensDecryptionKey.Text = null;
            SetNextButtonFocus();
            ValidateForm();
        }
    }
}
