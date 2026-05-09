using Microsoft.Win32;
using IWshRuntimeLibrary;

namespace Forever
{
    public static class AppInstaller
    {
        public static void RegisterForUninstall(
            string appName,
            string displayName,
            string publisher,
            string version,
            string uninstallExePath, // Path to your uninstaller
            string? installLocation = null)
        {
            string keyName = $@"Software\Microsoft\Windows\CurrentVersion\Uninstall\{appName}";

            using RegistryKey? key = Registry.LocalMachine.CreateSubKey(keyName);

            if (key == null)
                throw new Exception("Failed to create uninstall registry key");

            key.SetValue("DisplayName", displayName);
            key.SetValue("Publisher", publisher);
            key.SetValue("DisplayVersion", version);
            key.SetValue("InstallDate", DateTime.Now.ToString("yyyyMMdd"));
            key.SetValue("UninstallString", $"\"{uninstallExePath}\" --uninstall");
            key.SetValue("QuietUninstallString", $"\"{uninstallExePath}\" --uninstall --quiet");

            if (!string.IsNullOrEmpty(installLocation))
            {
                key.SetValue("InstallLocation", installLocation);
                key.SetValue("DisplayIcon", uninstallExePath);
            }

            key.SetValue("NoModify", 1);
            key.SetValue("NoRepair", 1);
        }

        public static void UnregisterFromUninstall(string appName)
        {
            string keyName = $@"Software\Microsoft\Windows\CurrentVersion\Uninstall\{appName}";
            Registry.LocalMachine.DeleteSubKeyTree(keyName, false);
        }

        public static void CreateDesktopShortcut(string shortcutName, string targetPath,
        string? arguments = null, string? description = null, string? iconLocation = null)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
            CreateShortcut(Path.Combine(desktop, $"{shortcutName}.lnk"), targetPath, arguments, description, iconLocation);
        }
        public static void CreateStartMenuShortcut(string shortcutName, string targetPath,
            string? subFolder = null, string? arguments = null, string? description = null, string? iconLocation = null)
        {
            string programs = Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms);
            string folder = string.IsNullOrWhiteSpace(subFolder)
                ? programs
                : Path.Combine(programs, subFolder);

            Directory.CreateDirectory(folder);

            CreateShortcut(Path.Combine(folder, $"{shortcutName}.lnk"), targetPath, arguments, description, iconLocation);
        }

        private static void CreateShortcut(string shortcutPath, string targetPath,
            string? arguments, string? description, string? iconLocation)
        {
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

            shortcut.TargetPath = targetPath;
            shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);
            shortcut.Arguments = arguments ?? "";
            shortcut.Description = description ?? "";

            if (!string.IsNullOrWhiteSpace(iconLocation))
                shortcut.IconLocation = iconLocation;

            shortcut.Save();
        }
    }
}