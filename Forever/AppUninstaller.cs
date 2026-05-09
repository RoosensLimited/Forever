using System.Diagnostics;
using IWshRuntimeLibrary;
using Microsoft.Win32;

namespace Forever
{
    public static class AppUninstaller
    {
        public static void PerformUninstall(string appKey, string displayName, string installFolder)
        {
            try
            {
                UnregisterFromUninstall(appKey);
                DeleteAllUsersShortcuts(displayName);
                CreateSelfDeleteBatch(installFolder);

                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Uninstall failed:\n\n{ex.Message}",
                    "Uninstall Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void UnregisterFromUninstall(string appKey)
        {
            try
            {
                string keyName = $@"Software\Microsoft\Windows\CurrentVersion\Uninstall\{appKey}";
                Registry.LocalMachine.DeleteSubKeyTree(keyName, false);
            }
            catch { }
        }

        private static void DeleteAllUsersShortcuts(string shortcutName)
        {
            string commonDesktop = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
            SafeDelete(Path.Combine(commonDesktop, $"{shortcutName}.lnk"));

            string commonPrograms = Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms);

            SafeDelete(Path.Combine(commonPrograms, $"{shortcutName}.lnk"));
        }

        private static void SafeDelete(string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }
            catch { }
        }

        private static void CreateSelfDeleteBatch(string installFolder)
        {
            string batchPath = Path.Combine(Path.GetTempPath(), $"uninstall_{Guid.NewGuid():N}.bat");

            string batchContent = $@"@echo off
timeout /t 10 /nobreak >nul
rd /s /q ""{installFolder}""
del /f /q ""{batchPath}""
";

            try
            {
                System.IO.File.WriteAllText(batchPath, batchContent);

                Process.Start(new ProcessStartInfo
                {
                    FileName = batchPath,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = true
                });
            }
            catch { }
        }

        public static bool IsRunningAsAdmin()
        {
            using var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }
    }
}