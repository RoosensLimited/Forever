using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forever
{
    public partial class ForeverUnsignedFiles : Form
    {
        public ForeverUnsignedFiles(List<string> files)
        {
            InitializeComponent();

            files.Sort();

            StringBuilder sbFiles = new StringBuilder();
            foreach (string file in files)
            {
                sbFiles.AppendLine(file);
            }

            rtbFiles.Text = sbFiles.ToString();

            DialogResult = DialogResult.Abort;
        }

        private void bAbort_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bContinue_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Continue;
            Close();
        }

        private void ForeverUnsignedFiles_Shown(object sender, EventArgs e)
        {
            SystemSounds.Asterisk.Play();
        }
    }
}
