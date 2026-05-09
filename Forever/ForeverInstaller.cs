namespace Forever
{
    public partial class ForeverInstaller : Form
    {
        internal bool Busy = false;
        public ForeverInstaller()
        {
            ForeverInstallerSettings.MainForm = this;

            InitializeComponent();
            SetNextUserControl();

            Text = ForeverSettings.GameName + " installer - Forever";
        }

        private void bQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bNext_Click(object sender, EventArgs e)
        {
            SetNextUserControl();
        }

        private void bPrevious_Click(object sender, EventArgs e)
        {
            if (Busy)
            {
                if (MessageBox.Show("The installation is running. Are you sure you want to abort?", "Forever", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            SetPreviousUserControl();
        }

        private void SetPreviousUserControl()
        {
            if (pParent.Controls.Count == 0)
            {
                return;
            }

            ucTemplateInstaller control;

            if (pParent.Controls[0] is ucInstallerSettings)
            {
                control = new ucInstallerWelcome();
            }
            else if (pParent.Controls[0] is ucInstallerRoosensDecryptionKey)
            {
                control = new ucInstallerSettings();
            }
            else if (pParent.Controls[0] is ucInstallerInstallation)
            {
                if (ForeverSettings.RoosensExecutablesDetected)
                {
                    control = new ucInstallerRoosensDecryptionKey();
                }
                else
                {
                    control = new ucInstallerSettings();
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            SetUserControl(control);
            control.ValidateForm();
        }

        private void SetNextUserControl()
        {
            ucTemplateInstaller control;

            if (pParent.Controls.Count == 0)
            {
                control = new ucInstallerWelcome();
            }
            else if (pParent.Controls[0] is ucInstallerWelcome)
            {
                control = new ucInstallerSettings();
            }
            else if (pParent.Controls[0] is ucInstallerSettings)
            {
                if (ForeverSettings.RoosensExecutablesDetected)
                {
                    control = new ucInstallerRoosensDecryptionKey();
                }
                else
                {
                    control = new ucInstallerInstallation();
                }
            }
            else if (pParent.Controls[0] is ucInstallerRoosensDecryptionKey)
            {
                control = new ucInstallerInstallation();
            }
            else if (pParent.Controls[0] is ucInstallerInstallation)
            {
                Application.Exit();
                return;
            }
            else
            {
                throw new NotImplementedException();
            }

            SetUserControl(control);
            control.ValidateForm();
        }

        private void SetUserControl(ucTemplateInstaller control)
        {
            bPrevious.Visible = !(control is ucInstallerWelcome)
                && !(control is ucInstallerInstallation);

            foreach (Control c in pParent.Controls.Cast<Control>().ToList())
            {
                pParent.Controls.Remove(c);
                c.Dispose();
            }

            control.Dock = DockStyle.Fill;

            pParent.Controls.Add(control);

            if (control.Tag is string)
            {
                SetTitle($"{control.Tag}");
            }
        }

        internal void SetTitle(string title, bool showPanel = true)
        {
            lTitle.Text = title;
            pParent.Visible = showPanel;
        }

        internal void SetNextButtonEnabled(bool enabled)
        {
            bNext.Enabled = enabled;
        }

        internal void SetNextButtonFocus()
        {
            bNext.Select();
            bNext.Focus();
        }

        internal void SetNextButtonText(string text)
        {
            bNext.Text = text;
        }

        internal void SetNextButtonVisible(bool visible)
        {
            bNext.Visible = visible;
        }

        private void ForeverInstaller_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Busy)
            {
                if (MessageBox.Show("The installation is running. Are you sure you want to quit?", "Forever", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
