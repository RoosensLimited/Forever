namespace Forever
{
    public partial class ucWelcome : ucTemplate
    {
        private bool scrolledToBottom = false;

        internal string InstallationDirectory;
        internal string GameName;

        public ucWelcome()
        {
            InitializeComponent();

            SetNextButtonText("&Agree");

            scrolledToBottom = ForeverSettings.AgreementAccepted;
            cbIAgree.Visible = ForeverSettings.AgreementAccepted;
            cbIAgree.Checked = ForeverSettings.AgreementAccepted;

            rtbAgreement.VScroll += control_Changed;
            cbIAgree.CheckedChanged += control_Changed;
        }

        private void control_Changed(object sender, EventArgs e)
        {
            ValidateForm();
        }

        public bool IsScrolledToBottom(RichTextBox rtb)
        {
            if (rtb == null || rtb.TextLength == 0)
            {
                return true;
            }

            var bottomRight = new Point(rtb.ClientRectangle.Right - 1, rtb.ClientRectangle.Bottom - 1);
            var lastVisibleCharIndex = rtb.GetCharIndexFromPosition(bottomRight);

            return lastVisibleCharIndex >= rtb.TextLength - 1;
        }

        internal override void ValidateForm()
        {
            if (IsScrolledToBottom(rtbAgreement))
            {
                scrolledToBottom = true;
                cbIAgree.Visible = true;
                cbIAgree.Select();
                cbIAgree.Focus();
            }

            ForeverSettings.AgreementAccepted = cbIAgree.Checked && scrolledToBottom;

            SetNextButtonEnabled(ForeverSettings.AgreementAccepted);
        }
    }
}
