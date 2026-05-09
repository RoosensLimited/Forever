namespace Forever
{
    partial class ForeverUnsignedFiles
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lTitle = new Label();
            bContinue = new Button();
            bAbort = new Button();
            rtbFiles = new RichTextBox();
            SuspendLayout();
            // 
            // lTitle
            // 
            lTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lTitle.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lTitle.Location = new Point(12, 9);
            lTitle.Name = "lTitle";
            lTitle.Size = new Size(584, 84);
            lTitle.TabIndex = 5;
            lTitle.Text = "The following files are not digitally signed. Unsigned files may be harmful or tampered with. Only continue if you fully trust the publisher.";
            // 
            // bContinue
            // 
            bContinue.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bContinue.FlatStyle = FlatStyle.System;
            bContinue.Location = new Point(440, 355);
            bContinue.Name = "bContinue";
            bContinue.Size = new Size(75, 23);
            bContinue.TabIndex = 6;
            bContinue.Text = "&Continue";
            bContinue.UseVisualStyleBackColor = true;
            bContinue.Click += bContinue_Click;
            // 
            // bAbort
            // 
            bAbort.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bAbort.FlatStyle = FlatStyle.System;
            bAbort.Location = new Point(521, 355);
            bAbort.Name = "bAbort";
            bAbort.Size = new Size(75, 23);
            bAbort.TabIndex = 7;
            bAbort.Text = "Abort";
            bAbort.UseVisualStyleBackColor = true;
            bAbort.Click += bAbort_Click;
            // 
            // rtbFiles
            // 
            rtbFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbFiles.BorderStyle = BorderStyle.None;
            rtbFiles.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbFiles.Location = new Point(12, 96);
            rtbFiles.Name = "rtbFiles";
            rtbFiles.Size = new Size(584, 243);
            rtbFiles.TabIndex = 8;
            rtbFiles.Text = "";
            // 
            // ForeverUnsignedFiles
            // 
            AcceptButton = bContinue;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = bAbort;
            ClientSize = new Size(608, 390);
            Controls.Add(rtbFiles);
            Controls.Add(bAbort);
            Controls.Add(bContinue);
            Controls.Add(lTitle);
            MinimumSize = new Size(624, 429);
            Name = "ForeverUnsignedFiles";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Forever";
            Shown += ForeverUnsignedFiles_Shown;
            ResumeLayout(false);
        }

        #endregion
        private Label lTitle;
        private Button bContinue;
        private Button bAbort;
        private RichTextBox rtbFiles;
    }
}