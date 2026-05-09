namespace Forever
{
    partial class ucOpenGameExecutable
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
            tbGameExecutable = new TextBox();
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
            // tbGameExecutable
            // 
            tbGameExecutable.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbGameExecutable.BorderStyle = BorderStyle.FixedSingle;
            tbGameExecutable.Location = new Point(1, 2);
            tbGameExecutable.Name = "tbGameExecutable";
            tbGameExecutable.ReadOnly = true;
            tbGameExecutable.Size = new Size(641, 23);
            tbGameExecutable.TabIndex = 5;
            // 
            // ucOpenGameExecutable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(bBrowse);
            Controls.Add(tbGameExecutable);
            Name = "ucOpenGameExecutable";
            Size = new Size(723, 352);
            Tag = "Open game launch executable";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bBrowse;
        private TextBox tbGameExecutable;
    }
}
