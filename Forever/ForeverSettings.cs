using System.Text.Json;
using System.Text.Json.Serialization;

namespace Forever
{
    internal enum ArchivePartSize
    {
        _50MB,
        _100MB,
        _250MB,
        _500MB,
        _1GB,
    }

    internal static class ForeverSettings
    {
        internal static bool AgreementAccepted = false;
        internal static string? InstallationDirectory;
        internal static string? GameName;
        internal static bool IsSteamGame;
        internal static string? GameLaunchExecutablePath;
        internal static string? RedistributablesDirectory;
        internal static bool IsSteamRedistributables;
        internal static string? RoosensDecryptionKey;
        internal static bool RoosensExecutablesDetected;
        internal static string? OutputDirectory;
        internal static Forever? MainForm;
        internal static ArchivePartSize PartSize = ArchivePartSize._1GB;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        private static string GetLastFolderName(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return string.Empty;

            return new DirectoryInfo(path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar))
                   .Name;
        }

        internal static bool SaveJson(string filePath)
        {
            try
            {
                var dto = new ForeverSettingsDto
                {
                    AgreementAccepted = AgreementAccepted,
                    InstallationDirectory = GetLastFolderName(InstallationDirectory),
                    GameName = GameName,
                    GameLaunchExecutablePath = GameLaunchExecutablePath.Substring(InstallationDirectory.Length),
                    RoosensExecutablesDetected = RoosensExecutablesDetected,
                };

                dto.GameLaunchExecutablePath = dto.GameLaunchExecutablePath.TrimStart("\\/".ToCharArray());

                string json = JsonSerializer.Serialize(dto, JsonOptions);
                File.WriteAllText(filePath, json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal static bool LoadJson(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return false;

                string json = File.ReadAllText(filePath);
                var dto = JsonSerializer.Deserialize<ForeverSettingsDto>(json);

                if (dto == null)
                    return false;

                InstallationDirectory = dto.InstallationDirectory;
                GameName = dto.GameName;
                GameLaunchExecutablePath = dto.GameLaunchExecutablePath;
                RoosensExecutablesDetected = dto.RoosensExecutablesDetected;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private class ForeverSettingsDto
        {
            public bool AgreementAccepted { get; set; } = false;
            public string? InstallationDirectory { get; set; }
            public string? GameName { get; set; }
            public string? GameLaunchExecutablePath { get; set; }
            public bool RoosensExecutablesDetected { get; set; }
        }
    }
}