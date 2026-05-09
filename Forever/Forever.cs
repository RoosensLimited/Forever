namespace Forever
{
    public partial class Forever : Form
    {
        internal bool Busy = false;

        public Forever()
        {
            ForeverSettings.MainForm = this;

            InitializeComponent();
            SetNextUserControl();
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
                if (MessageBox.Show("The installer is being created. Are you sure you want to abort?", "Forever", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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

            ucTemplate control;

            if (pParent.Controls[0] is ucSelectGameFolder)
            {
                control = new ucWelcome();
            }
            else if (pParent.Controls[0] is ucOpenGameExecutable)
            {
                control = new ucSelectGameFolder();
            }
            else if (pParent.Controls[0] is ucRoosensDecryptionKey)
            {
                control = new ucOpenGameExecutable();
            }
            else if (pParent.Controls[0] is ucSelectRedistributables)
            {
                control = new ucRoosensDecryptionKey();
            }
            else if (pParent.Controls[0] is ucSelectOutputFolder)
            {
                control = new ucSelectRedistributables();
            }
            else if (pParent.Controls[0] is ucCreate)
            {
                control = new ucSelectOutputFolder();
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
            ucTemplate control;

            if (pParent.Controls.Count == 0)
            {
                control = new ucWelcome();
            }
            else if (pParent.Controls[0] is ucWelcome)
            {
                control = new ucSelectGameFolder();
            }
            else if (pParent.Controls[0] is ucSelectGameFolder)
            {
                control = new ucOpenGameExecutable();
            }
            else if (pParent.Controls[0] is ucOpenGameExecutable)
            {
                control = new ucRoosensDecryptionKey();
            }
            else if (pParent.Controls[0] is ucRoosensDecryptionKey)
            {
                control = new ucSelectRedistributables();
            }
            else if (pParent.Controls[0] is ucSelectRedistributables)
            {
                control = new ucSelectOutputFolder();
            }
            else if (pParent.Controls[0] is ucSelectOutputFolder)
            {
                control = new ucCreate();
            }
            else if (pParent.Controls[0] is ucCreate)
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

        private void SetUserControl(ucTemplate control)
        {
            bPrevious.Visible = !(control is ucWelcome);

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

        private void Forever_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Busy)
            {
                if (MessageBox.Show("The installer is being created. Are you sure you want to quit?", "Forever", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
