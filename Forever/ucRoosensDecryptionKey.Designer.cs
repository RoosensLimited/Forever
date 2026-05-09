namespace Forever
{
    partial class ucRoosensDecryptionKey
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
            lRoosensDecyptionKey = new Label();
            tbRoosensDecryptionKey = new TextBox();
            bwFindRoosensExecutable = new System.ComponentModel.BackgroundWorker();
            lRoosensInfo = new Label();
            pbFindRoosensExecutables = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbFindRoosensExecutables).BeginInit();
            SuspendLayout();
            // 
            // lRoosensDecyptionKey
            // 
            lRoosensDecyptionKey.AutoSize = true;
            lRoosensDecyptionKey.Location = new Point(3, 5);
            lRoosensDecyptionKey.Name = "lRoosensDecyptionKey";
            lRoosensDecyptionKey.Size = new Size(0, 15);
            lRoosensDecyptionKey.TabIndex = 0;
            // 
            // tbRoosensDecryptionKey
            // 
            tbRoosensDecryptionKey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbRoosensDecryptionKey.BorderStyle = BorderStyle.FixedSingle;
            tbRoosensDecryptionKey.Location = new Point(1, 3);
            tbRoosensDecryptionKey.MaxLength = 128;
            tbRoosensDecryptionKey.Name = "tbRoosensDecryptionKey";
            tbRoosensDecryptionKey.Size = new Size(721, 23);
            tbRoosensDecryptionKey.TabIndex = 6;
            tbRoosensDecryptionKey.Visible = false;
            // 
            // bwFindRoosensExecutable
            // 
            bwFindRoosensExecutable.WorkerSupportsCancellation = true;
            bwFindRoosensExecutable.DoWork += bwFindRoosensExecutable_DoWork;
            bwFindRoosensExecutable.RunWorkerCompleted += bwFindRoosensExecutable_RunWorkerCompleted;
            // 
            // lRoosensInfo
            // 
            lRoosensInfo.AutoSize = true;
            lRoosensInfo.Location = new Point(3, 29);
            lRoosensInfo.Name = "lRoosensInfo";
            lRoosensInfo.Size = new Size(0, 15);
            lRoosensInfo.TabIndex = 8;
            // 
            // pbFindRoosensExecutables
            // 
            pbFindRoosensExecutables.Anchor = AnchorStyles.None;
            pbFindRoosensExecutables.Location = new Point(299, 132);
            pbFindRoosensExecutables.Name = "pbFindRoosensExecutables";
            pbFindRoosensExecutables.Size = new Size(125, 88);
            pbFindRoosensExecutables.SizeMode = PictureBoxSizeMode.Zoom;
            pbFindRoosensExecutables.TabIndex = 9;
            pbFindRoosensExecutables.TabStop = false;
            // 
            // ucRoosensDecryptionKey
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pbFindRoosensExecutables);
            Controls.Add(lRoosensInfo);
            Controls.Add(tbRoosensDecryptionKey);
            Controls.Add(lRoosensDecyptionKey);
            Name = "ucRoosensDecryptionKey";
            Size = new Size(723, 352);
            Tag = "Roosens Decryption Key";
            ((System.ComponentModel.ISupportInitialize)pbFindRoosensExecutables).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lRoosensDecyptionKey;
        private TextBox tbRoosensDecryptionKey;
        private System.ComponentModel.BackgroundWorker bwFindRoosensExecutable;
        private Label lRoosensInfo;
        private PictureBox pbFindRoosensExecutables;
    }
}
