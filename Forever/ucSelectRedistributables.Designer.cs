namespace Forever
{
    partial class ucSelectRedistributables
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
            rbSteam = new RadioButton();
            rbCustomFolder = new RadioButton();
            cbSteamRedistributables = new ComboBox();
            bBrowse = new Button();
            tbCustomFolder = new TextBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // rbSteam
            // 
            rbSteam.AutoSize = true;
            rbSteam.Enabled = false;
            rbSteam.Location = new Point(3, 3);
            rbSteam.Name = "rbSteam";
            rbSteam.Size = new Size(145, 19);
            rbSteam.TabIndex = 0;
            rbSteam.TabStop = true;
            rbSteam.Text = "Steam Redistributables";
            rbSteam.UseVisualStyleBackColor = true;
            // 
            // rbCustomFolder
            // 
            rbCustomFolder.AutoSize = true;
            rbCustomFolder.Location = new Point(3, 57);
            rbCustomFolder.Name = "rbCustomFolder";
            rbCustomFolder.Size = new Size(103, 19);
            rbCustomFolder.TabIndex = 1;
            rbCustomFolder.TabStop = true;
            rbCustomFolder.Text = "Custom Folder";
            rbCustomFolder.UseVisualStyleBackColor = true;
            // 
            // cbSteamRedistributables
            // 
            cbSteamRedistributables.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbSteamRedistributables.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSteamRedistributables.Enabled = false;
            cbSteamRedistributables.FormattingEnabled = true;
            cbSteamRedistributables.Location = new Point(154, 2);
            cbSteamRedistributables.Name = "cbSteamRedistributables";
            cbSteamRedistributables.Size = new Size(568, 23);
            cbSteamRedistributables.TabIndex = 2;
            // 
            // bBrowse
            // 
            bBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bBrowse.FlatStyle = FlatStyle.System;
            bBrowse.Location = new Point(648, 57);
            bBrowse.Name = "bBrowse";
            bBrowse.Size = new Size(75, 23);
            bBrowse.TabIndex = 4;
            bBrowse.Text = "&Browse";
            bBrowse.UseVisualStyleBackColor = true;
            bBrowse.Click += bBrowse_Click;
            // 
            // tbCustomFolder
            // 
            tbCustomFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbCustomFolder.BorderStyle = BorderStyle.FixedSingle;
            tbCustomFolder.Location = new Point(154, 57);
            tbCustomFolder.Name = "tbCustomFolder";
            tbCustomFolder.ReadOnly = true;
            tbCustomFolder.Size = new Size(488, 23);
            tbCustomFolder.TabIndex = 3;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(35, 314);
            label1.Name = "label1";
            label1.Size = new Size(382, 15);
            label1.TabIndex = 5;
            label1.Text = "The generated installer will run every executable found in this directory.";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBox1.Location = new Point(12, 302);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(20, 38);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // ucSelectRedistributables
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(bBrowse);
            Controls.Add(tbCustomFolder);
            Controls.Add(cbSteamRedistributables);
            Controls.Add(rbCustomFolder);
            Controls.Add(rbSteam);
            Name = "ucSelectRedistributables";
            Size = new Size(723, 352);
            Tag = "Select root of redistributables";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton rbSteam;
        private RadioButton rbCustomFolder;
        private ComboBox cbSteamRedistributables;
        private Button bBrowse;
        private TextBox tbCustomFolder;
        private Label label1;
        private PictureBox pictureBox1;
    }
}
