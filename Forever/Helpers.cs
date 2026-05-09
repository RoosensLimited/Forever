using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;

namespace Forever
{
    internal static class Helpers
    {
        internal static string GetExecutablePath()
        {
            if (!string.IsNullOrEmpty(Environment.ProcessPath))
                return Environment.ProcessPath;

            try
            {
                return Process.GetCurrentProcess().MainModule?.FileName
                       ?? Assembly.GetExecutingAssembly().Location;
            }
            catch
            {
                return Assembly.GetExecutingAssembly().Location;
            }
        }

        internal static List<string> FindExeAndDllFiles(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");
            }

            try
            {
                return Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                                .Where(file =>
                                    file.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) ||
                                    file.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                                .ToList();
            }
            catch (UnauthorizedAccessException)
            {
                return new List<string>();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        internal static string FindAfterString(string content, string search, string? separators, bool caseSensitive = true)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(search))
            {
                return null;
            }

            int index = -1;

            if (caseSensitive)
            {
                index = content.IndexOf(search);
            }
            else
            {
                index = content.ToUpper().IndexOf(search.ToUpper());
            }

            if (index < 0)
            {
                return null;
            }

            content = content.Substring(index + search.Length);

            if (string.IsNullOrEmpty(separators))
            {
                return content;
            }

            return content.Split(separators.ToCharArray(), StringSplitOptions.None).First();
        }

        internal static bool IsRunningAsAdmin()
        {
            using var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        internal static void RestartAsAdmin()
        {
            if (IsRunningAsAdmin())
                return;

            string arguments = string.Join(" ", Environment.GetCommandLineArgs().Skip(1));

            var processInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = GetExecutablePath(),
                Verb = "runas",
                Arguments = arguments,
            };

            try
            {
                Process.Start(processInfo);
                
            }
            catch (Exception)
            {
            }
            finally
            {
                Environment.Exit(0);
            }
        }

        public static bool IsValidFolderPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            if (path.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                return false;

            try
            {
                string fullPath = Path.GetFullPath(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsDirectoryEmpty(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            if (!Directory.Exists(path))
                return false;

            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        public static int RunProcess(string exePath, string arguments)
        {
            if (string.IsNullOrWhiteSpace(exePath))
                throw new ArgumentException("Executable path cannot be empty", nameof(exePath));

            ProcessStartInfo startInfo = null;
            if (exePath.ToLower().EndsWith(".cmd"))
            {
                startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/s /c \"{exePath}\" {arguments ?? ""}",
                    UseShellExecute = false,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = true,
                    WorkingDirectory = Path.GetDirectoryName(exePath)
                };
            }
            else
            {
                startInfo = new ProcessStartInfo
                {
                    FileName = exePath,
                    Arguments = arguments ?? string.Empty,
                    UseShellExecute = false,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = true,
                    WorkingDirectory = Path.GetDirectoryName(exePath)
                };
            }

                using var process = Process.Start(startInfo)
                    ?? throw new InvalidOperationException($"Failed to start process: {exePath}");

            process.WaitForExit();
            return process.ExitCode;
        }
    }
}
