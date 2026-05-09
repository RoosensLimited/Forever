namespace Forever
{
    partial class ucSelectOutputFolder
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
            tbOutputFolder = new TextBox();
            cbSplit = new ComboBox();
            SuspendLayout();
            // 
            // bBrowse
            // 
            bBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bBrowse.FlatStyle = FlatStyle.System;
            bBrowse.Location = new Point(648, 2);
            bBrowse.Name = "bBrowse";
            bBrowse.Size = new Size(75, 23);
            bBrowse.TabIndex = 8;
            bBrowse.Text = "&Browse";
            bBrowse.UseVisualStyleBackColor = true;
            bBrowse.Click += bBrowse_Click;
            // 
            // tbOutputFolder
            // 
            tbOutputFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbOutputFolder.BorderStyle = BorderStyle.FixedSingle;
            tbOutputFolder.Location = new Point(1, 2);
            tbOutputFolder.Name = "tbOutputFolder";
            tbOutputFolder.ReadOnly = true;
            tbOutputFolder.Size = new Size(641, 23);
            tbOutputFolder.TabIndex = 7;
            // 
            // cbSplit
            // 
            cbSplit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbSplit.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSplit.FormattingEnabled = true;
            cbSplit.Items.AddRange(new object[] { "Split into 50MB parts", "Split into 100MB parts", "Split into 250MB parts", "Split into 500MB parts", "Split into 1GB parts" });
            cbSplit.Location = new Point(1, 31);
            cbSplit.Name = "cbSplit";
            cbSplit.Size = new Size(721, 23);
            cbSplit.TabIndex = 9;
            // 
            // ucSelectOutputFolder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(cbSplit);
            Controls.Add(bBrowse);
            Controls.Add(tbOutputFolder);
            Name = "ucSelectOutputFolder";
            Size = new Size(723, 352);
            Tag = "Select output folder";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bBrowse;
        private TextBox tbOutputFolder;
        private ComboBox cbSplit;
    }
}
