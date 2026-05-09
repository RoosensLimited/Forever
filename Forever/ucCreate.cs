using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using Ionic.Zip;

namespace Forever
{
    public partial class ucCreate : ucTemplate
    {
        public ucCreate()
        {
            InitializeComponent();

            ApplyDarkModeToControls(this);

            SetNextButtonText("&Close");
            SetNextButtonEnabled(false);

            bwCreate.RunWorkerAsync();

            VisibleChanged += control_Changed;
        }

        private void control_Changed(object? sender, EventArgs e)
        {
            if (!Visible)
            {
                if (bwCreate != null && bwCreate.IsBusy)
                {
                    try
                    {
                        bwCreate.CancelAsync();
                    }
                    catch { }
                }
            }
        }

        internal override void ValidateForm()
        {
            SetNextButtonEnabled(false);
        }

        private class ZipParams
        {
            public string SourceFolder { get; set; }
            public string ZipBasePath { get; set; }
            public int SplitSizeMB { get; set; } = 100;
        }

        private void bwCreate_DoWork(object sender, DoWorkEventArgs e)
        {
            ForeverSettings.MainForm.Busy = true;

            try
            {
                if (!Directory.Exists(ForeverSettings.OutputDirectory))
                {
                    Directory.CreateDirectory(ForeverSettings.OutputDirectory);
                }

                int maxSegmentSize = 0;

                switch (ForeverSettings.PartSize)
                {
                    case ArchivePartSize._50MB:
                        maxSegmentSize = 50 * 1024 * 1024;
                        break;
                    case ArchivePartSize._100MB:
                        maxSegmentSize = 100 * 1024 * 1024;
                        break;
                    case ArchivePartSize._250MB:
                        maxSegmentSize = 250 * 1024 * 1024;
                        break;
                    case ArchivePartSize._500MB:
                        maxSegmentSize = 500 * 1024 * 1024;
                        break;
                    case ArchivePartSize._1GB:
                        maxSegmentSize = 1024 * 1024 * 1024;
                        break;

                }

                using (var zip = new ZipFile())
                {
                    zip.AlternateEncoding = System.Text.Encoding.UTF8;
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;

                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Default;
                    zip.UseZip64WhenSaving = Zip64Option.Always;
                    zip.MaxOutputSegmentSize = maxSegmentSize;

                    zip.TempFileFolder = Path.GetDirectoryName(ForeverSettings.OutputDirectory);

                    zip.AddDirectory(ForeverSettings.InstallationDirectory, string.Empty);

                    zip.SaveProgress += (s, args) =>
                    {
                        if (bwCreate.CancellationPending)
                        {
                            args.Cancel = true;
                            return;
                        }
                    };

                    zip.Save(Path.Combine(ForeverSettings.OutputDirectory, "game.zip"));
                }


                using (var zip = new ZipFile())
                {
                    zip.AlternateEncoding = System.Text.Encoding.UTF8;
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;

                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Default;
                    zip.UseZip64WhenSaving = Zip64Option.Always;
                    zip.MaxOutputSegmentSize = maxSegmentSize;

                    zip.TempFileFolder = Path.GetDirectoryName(ForeverSettings.OutputDirectory);

                    zip.AddDirectory(ForeverSettings.RedistributablesDirectory, string.Empty);

                    zip.SaveProgress += (s, args) =>
                    {
                        if (bwCreate.CancellationPending)
                        {
                            args.Cancel = true;
                            return;
                        }
                    };

                    zip.Save(Path.Combine(ForeverSettings.OutputDirectory, "redist.zip"));
                }

                if (ForeverSettings.RoosensExecutablesDetected)
                {
                    if (!string.IsNullOrEmpty(ForeverSettings.RoosensDecryptionKey))
                    {
                        File.WriteAllText(Path.Combine(ForeverSettings.OutputDirectory, "decryptionkey.txt"), ForeverSettings.RoosensDecryptionKey);
                    }
                    else
                    {
                        File.WriteAllText(Path.Combine(ForeverSettings.OutputDirectory, "decryptionkey.txt"), string.Empty);
                    }
                }

                var exePath = Helpers.GetExecutablePath();
                if (string.IsNullOrEmpty(exePath))
                {
                    throw new Exception("Could not get exe path");
                }

                if (!ForeverSettings.SaveJson(Path.Combine(ForeverSettings.OutputDirectory, "setup.json")))
                {
                    throw new Exception("Could not save settings");
                }

                File.Copy(exePath, Path.Combine(ForeverSettings.OutputDirectory, "setup.exe"), true);

            }
            catch (Exception ex)
            {
               e.Result = "Error: " + ex.Message;
            }
        }

        private void bwCreate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ForeverSettings.MainForm.Busy = false;

            pbCreating.Visible = false;
            SetNextButtonEnabled(true);

            if (e.Result != null && e.Result is string)
            {
                ForeverSettings.MainForm.SetTitle((string)e.Result, false);
                return;
            }

            ForeverSettings.MainForm.SetTitle("Finished!");
        }
    }
}