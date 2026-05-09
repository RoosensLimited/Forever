namespace Forever
{
    public class ucTemplateInstaller : ucTemplate
    {
        public ucTemplateInstaller()
        {
            if (ForeverInstallerSettings.MainForm != null)
            {
                SetNextButtonFocus();
            }
        }

        protected new void SetNextButtonEnabled(bool enabled)
        {
            ForeverInstallerSettings.MainForm.SetNextButtonEnabled(enabled);
        }

        internal new void SetNextButtonFocus()
        {
            ForeverInstallerSettings.MainForm.SetNextButtonFocus();
        }
        protected new void SetNextButtonText(string text)
        {
            ForeverInstallerSettings.MainForm.SetNextButtonText(text);
        }

        internal virtual void ValidateForm()
        {
            throw new NotImplementedException();
        }

        protected void ApplyDarkModeToControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox txt)
                {
                    txt.BackColor = SystemColors.Window;
                    txt.ForeColor = SystemColors.WindowText;
                }
                else if (c is RichTextBox rtb)
                {
                    rtb.BackColor = SystemColors.Window;
                    rtb.ForeColor = SystemColors.WindowText;
                }
                else if (c is ProgressBar pb)
                {
                    pb.BackColor = pb.Parent?.BackColor ?? SystemColors.Control;
                    pb.ForeColor = SystemColors.Highlight;

                    pb.Invalidate(true);
                    pb.Update();
                }

                // Recurse into containers
                if (c.HasChildren)
                {
                    ApplyDarkModeToControls(c);
                }
            }
        }
    }
}
