namespace Forever
{
    public class ucTemplate : UserControl
    {
        public ucTemplate()
        {
        }

        protected void SetNextButtonEnabled(bool enabled)
        {
            ForeverSettings.MainForm.SetNextButtonEnabled(enabled);
        }

        protected void SetNextButtonFocus()
        {
            ForeverSettings.MainForm.SetNextButtonFocus();
        }

        protected void SetNextButtonText(string text)
        {
            ForeverSettings.MainForm.SetNextButtonText(text);
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
