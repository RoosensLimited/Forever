namespace Forever
{
    partial class ucInstallerWelcome
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucInstallerWelcome));
            rtbAgreement = new RichTextBox();
            cbIAgree = new CheckBox();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // rtbAgreement
            // 
            rtbAgreement.BorderStyle = BorderStyle.None;
            rtbAgreement.Dock = DockStyle.Fill;
            rtbAgreement.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbAgreement.Location = new Point(0, 0);
            rtbAgreement.Name = "rtbAgreement";
            rtbAgreement.Size = new Size(904, 571);
            rtbAgreement.TabIndex = 0;
            rtbAgreement.Text = resources.GetString("rtbAgreement.Text");
            // 
            // cbIAgree
            // 
            cbIAgree.AutoSize = true;
            cbIAgree.Location = new Point(0, 21);
            cbIAgree.Name = "cbIAgree";
            cbIAgree.Size = new Size(415, 19);
            cbIAgree.TabIndex = 1;
            cbIAgree.Text = "&I fully read, fully understand and fully agree with the terms and conditions";
            cbIAgree.UseVisualStyleBackColor = true;
            cbIAgree.Visible = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(cbIAgree);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 571);
            panel1.Name = "panel1";
            panel1.Size = new Size(904, 59);
            panel1.TabIndex = 2;
            // 
            // ucInstallerWelcome
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rtbAgreement);
            Controls.Add(panel1);
            Name = "ucInstallerWelcome";
            Size = new Size(904, 630);
            Tag = "Own your games Forever";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rtbAgreement;
        private CheckBox cbIAgree;
        private Panel panel1;
    }
}
