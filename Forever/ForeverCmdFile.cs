using System.Media;
using System.Text;

namespace Forever
{
    public partial class ForeverCmdFile : Form
    {
        public ForeverCmdFile(string path)
        {
            InitializeComponent();

            StringBuilder sbCommand = new StringBuilder();
            sbCommand.AppendLine($"Path: {path}");
            

            try
            {
                var content = File.ReadAllLines(path);
                sbCommand.AppendLine(string.Empty);
                sbCommand.AppendLine("Content: ");
                foreach (var line in content)
                {
                    sbCommand.AppendLine(line);
                }
            }
            catch
            {

            }

            rtbFiles.Text = sbCommand.ToString();

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
