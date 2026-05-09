namespace Forever
{
    partial class ucCreate
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
            bwCreate = new System.ComponentModel.BackgroundWorker();
            pbCreating = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbCreating).BeginInit();
            SuspendLayout();
            // 
            // bwCreate
            // 
            bwCreate.WorkerReportsProgress = true;
            bwCreate.WorkerSupportsCancellation = true;
            bwCreate.DoWork += bwCreate_DoWork;
            bwCreate.RunWorkerCompleted += bwCreate_RunWorkerCompleted;
            // 
            // pbCreating
            // 
            pbCreating.Anchor = AnchorStyles.None;
            pbCreating.Image = global::Forever.Properties.Resources.loading;
            pbCreating.Location = new Point(299, 132);
            pbCreating.Name = "pbCreating";
            pbCreating.Size = new Size(125, 88);
            pbCreating.SizeMode = PictureBoxSizeMode.Zoom;
            pbCreating.TabIndex = 1;
            pbCreating.TabStop = false;
            // 
            // ucCreate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pbCreating);
            Name = "ucCreate";
            Size = new Size(723, 352);
            Tag = "Creating installer. Please wait...";
            ((System.ComponentModel.ISupportInitialize)pbCreating).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.BackgroundWorker bwCreate;
        private PictureBox pbCreating;
    }
}
