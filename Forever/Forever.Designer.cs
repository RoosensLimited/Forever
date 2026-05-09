namespace Forever
{
    partial class Forever
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Forever));
            pParent = new Panel();
            bNext = new Button();
            bPrevious = new Button();
            lTitle = new Label();
            SuspendLayout();
            // 
            // pParent
            // 
            pParent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pParent.Location = new Point(12, 51);
            pParent.Name = "pParent";
            pParent.Size = new Size(997, 293);
            pParent.TabIndex = 0;
            // 
            // bNext
            // 
            bNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bNext.FlatStyle = FlatStyle.System;
            bNext.Location = new Point(934, 359);
            bNext.Name = "bNext";
            bNext.Size = new Size(75, 23);
            bNext.TabIndex = 1;
            bNext.Text = "&Next";
            bNext.UseVisualStyleBackColor = true;
            bNext.Click += bNext_Click;
            // 
            // bPrevious
            // 
            bPrevious.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            bPrevious.FlatStyle = FlatStyle.System;
            bPrevious.Location = new Point(12, 359);
            bPrevious.Name = "bPrevious";
            bPrevious.Size = new Size(75, 23);
            bPrevious.TabIndex = 2;
            bPrevious.Text = "&Previous";
            bPrevious.UseVisualStyleBackColor = true;
            bPrevious.Visible = false;
            bPrevious.Click += bPrevious_Click;
            // 
            // lTitle
            // 
            lTitle.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lTitle.Location = new Point(12, 9);
            lTitle.Name = "lTitle";
            lTitle.Size = new Size(997, 335);
            lTitle.TabIndex = 4;
            lTitle.Text = "Title";
            // 
            // Forever
            // 
            AcceptButton = bNext;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = bPrevious;
            ClientSize = new Size(1021, 394);
            Controls.Add(pParent);
            Controls.Add(bPrevious);
            Controls.Add(bNext);
            Controls.Add(lTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Forever";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Forever";
            FormClosing += Forever_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private Panel pParent;
        private Button bNext;
        private Button bPrevious;
        private Label lTitle;
    }
}
