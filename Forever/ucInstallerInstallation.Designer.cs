namespace Forever
{
    partial class ucInstallerInstallation
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
            pbInstalling = new PictureBox();
            bwInstalling = new System.ComponentModel.BackgroundWorker();
            bwFinalizing = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)pbInstalling).BeginInit();
            SuspendLayout();
            // 
            // pbInstalling
            // 
            pbInstalling.Anchor = AnchorStyles.None;
            pbInstalling.Location = new Point(299, 132);
            pbInstalling.Name = "pbInstalling";
            pbInstalling.Size = new Size(125, 88);
            pbInstalling.SizeMode = PictureBoxSizeMode.Zoom;
            pbInstalling.TabIndex = 2;
            pbInstalling.TabStop = false;
            // 
            // bwInstalling
            // 
            bwInstalling.WorkerSupportsCancellation = true;
            bwInstalling.DoWork += bwInstalling_DoWork;
            bwInstalling.RunWorkerCompleted += bwInstalling_RunWorkerCompleted;
            // 
            // bwFinalizing
            // 
            bwFinalizing.WorkerSupportsCancellation = true;
            bwFinalizing.DoWork += bwFinalizing_DoWork;
            bwFinalizing.RunWorkerCompleted += bwFinalizing_RunWorkerCompleted;
            // 
            // ucInstallationBusy
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pbInstalling);
            Name = "ucInstallationBusy";
            Size = new Size(723, 352);
            Tag = "Installing. Please wait...";
            ((System.ComponentModel.ISupportInitialize)pbInstalling).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbInstalling;
        private System.ComponentModel.BackgroundWorker bwInstalling;
        private System.ComponentModel.BackgroundWorker bwFinalizing;
    }
}
