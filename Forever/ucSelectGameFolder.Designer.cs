namespace Forever
{
    partial class ucSelectGameFolder
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
            cbSteamGames = new ComboBox();
            bBrowse = new Button();
            tbCustomFolder = new TextBox();
            tbCustomFolderGameName = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // rbSteam
            // 
            rbSteam.AutoSize = true;
            rbSteam.Enabled = false;
            rbSteam.Location = new Point(3, 3);
            rbSteam.Name = "rbSteam";
            rbSteam.Size = new Size(92, 19);
            rbSteam.TabIndex = 0;
            rbSteam.TabStop = true;
            rbSteam.Text = "Steam Game";
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
            // cbSteamGames
            // 
            cbSteamGames.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbSteamGames.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSteamGames.Enabled = false;
            cbSteamGames.FormattingEnabled = true;
            cbSteamGames.Location = new Point(133, 2);
            cbSteamGames.Name = "cbSteamGames";
            cbSteamGames.Size = new Size(589, 23);
            cbSteamGames.TabIndex = 2;
            // 
            // bBrowse
            // 
            bBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bBrowse.FlatStyle = FlatStyle.System;
            bBrowse.Location = new Point(647, 57);
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
            tbCustomFolder.Location = new Point(133, 57);
            tbCustomFolder.Name = "tbCustomFolder";
            tbCustomFolder.ReadOnly = true;
            tbCustomFolder.Size = new Size(508, 23);
            tbCustomFolder.TabIndex = 3;
            tbCustomFolder.TextChanged += tbCustomFolder_TextChanged;
            // 
            // tbCustomFolderGameName
            // 
            tbCustomFolderGameName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbCustomFolderGameName.BorderStyle = BorderStyle.FixedSingle;
            tbCustomFolderGameName.Location = new Point(133, 86);
            tbCustomFolderGameName.Name = "tbCustomFolderGameName";
            tbCustomFolderGameName.Size = new Size(589, 23);
            tbCustomFolderGameName.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 88);
            label1.Name = "label1";
            label1.Size = new Size(73, 15);
            label1.TabIndex = 6;
            label1.Text = "Game Name";
            // 
            // ucSelectGameFolder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(tbCustomFolderGameName);
            Controls.Add(bBrowse);
            Controls.Add(tbCustomFolder);
            Controls.Add(cbSteamGames);
            Controls.Add(rbCustomFolder);
            Controls.Add(rbSteam);
            Name = "ucSelectGameFolder";
            Size = new Size(723, 352);
            Tag = "Select root of game directory";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton rbSteam;
        private RadioButton rbCustomFolder;
        private ComboBox cbSteamGames;
        private Button bBrowse;
        private TextBox tbCustomFolder;
        private TextBox tbCustomFolderGameName;
        private Label label1;
    }
}
