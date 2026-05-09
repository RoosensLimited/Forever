namespace Forever
{
    partial class ucInstallerSettings
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
            bBrowse = new Button();
            tbGameInstallationFolder = new TextBox();
            label1 = new Label();
            cbCreateDesktopShortcut = new CheckBox();
            cbCreateStartmenuShortcut = new CheckBox();
            cbCreateUninstaller = new CheckBox();
            SuspendLayout();
            // 
            // bBrowse
            // 
            bBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bBrowse.FlatStyle = FlatStyle.System;
            bBrowse.Location = new Point(648, 2);
            bBrowse.Name = "bBrowse";
            bBrowse.Size = new Size(75, 23);
            bBrowse.TabIndex = 6;
            bBrowse.Text = "&Browse";
            bBrowse.UseVisualStyleBackColor = true;
            bBrowse.Click += bBrowse_Click;
            // 
            // tbGameInstallationFolder
            // 
            tbGameInstallationFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbGameInstallationFolder.BorderStyle = BorderStyle.FixedSingle;
            tbGameInstallationFolder.Location = new Point(133, 2);
            tbGameInstallationFolder.Name = "tbGameInstallationFolder";
            tbGameInstallationFolder.ReadOnly = true;
            tbGameInstallationFolder.Size = new Size(509, 23);
            tbGameInstallationFolder.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 6);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 7;
            label1.Text = "Installation folder";
            // 
            // cbCreateDesktopShortcut
            // 
            cbCreateDesktopShortcut.AutoSize = true;
            cbCreateDesktopShortcut.Checked = true;
            cbCreateDesktopShortcut.CheckState = CheckState.Checked;
            cbCreateDesktopShortcut.Location = new Point(133, 75);
            cbCreateDesktopShortcut.Name = "cbCreateDesktopShortcut";
            cbCreateDesktopShortcut.Size = new Size(169, 19);
            cbCreateDesktopShortcut.TabIndex = 8;
            cbCreateDesktopShortcut.Text = "Create shortcut on &desktop";
            cbCreateDesktopShortcut.UseVisualStyleBackColor = true;
            // 
            // cbCreateStartmenuShortcut
            // 
            cbCreateStartmenuShortcut.AutoSize = true;
            cbCreateStartmenuShortcut.Checked = true;
            cbCreateStartmenuShortcut.CheckState = CheckState.Checked;
            cbCreateStartmenuShortcut.Location = new Point(133, 100);
            cbCreateStartmenuShortcut.Name = "cbCreateStartmenuShortcut";
            cbCreateStartmenuShortcut.Size = new Size(180, 19);
            cbCreateStartmenuShortcut.TabIndex = 9;
            cbCreateStartmenuShortcut.Text = "Create shortcut in &start menu";
            cbCreateStartmenuShortcut.UseVisualStyleBackColor = true;
            // 
            // cbCreateUninstaller
            // 
            cbCreateUninstaller.AutoSize = true;
            cbCreateUninstaller.Checked = true;
            cbCreateUninstaller.CheckState = CheckState.Checked;
            cbCreateUninstaller.Location = new Point(133, 125);
            cbCreateUninstaller.Name = "cbCreateUninstaller";
            cbCreateUninstaller.Size = new Size(118, 19);
            cbCreateUninstaller.TabIndex = 10;
            cbCreateUninstaller.Text = "Create &uninstaller";
            cbCreateUninstaller.UseVisualStyleBackColor = true;
            // 
            // ucInstallerSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(cbCreateUninstaller);
            Controls.Add(cbCreateStartmenuShortcut);
            Controls.Add(cbCreateDesktopShortcut);
            Controls.Add(label1);
            Controls.Add(bBrowse);
            Controls.Add(tbGameInstallationFolder);
            Name = "ucInstallerSettings";
            Size = new Size(723, 352);
            Tag = "Installation Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bBrowse;
        private TextBox tbGameInstallationFolder;
        private Label label1;
        private CheckBox cbCreateDesktopShortcut;
        private CheckBox cbCreateStartmenuShortcut;
        private CheckBox cbCreateUninstaller;
    }
}
