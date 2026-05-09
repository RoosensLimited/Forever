using System.Text;

namespace Forever
{
    internal class RemoveProtection
    {
        internal static bool HasRoosensSection(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                return false;

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fs.Length < 0x2000)
                        return false;

                    var buffer = new byte[0x2000];
                    var bytesRead = fs.Read(buffer, 0, 0x2000);

                    if (bytesRead < 0x2000)
                        return false;

                    if (buffer[0] != 0x4D || buffer[1] != 0x5A)
                        return false;

                    var peOffset = BitConverter.ToInt32(buffer, 0x3C);

                    if (peOffset < 0x40 || peOffset + 4 > 0x2000)
                        return false;

                    if (buffer[peOffset] != 0x50 || buffer[peOffset + 1] != 0x45 ||
                        buffer[peOffset + 2] != 0x00 || buffer[peOffset + 3] != 0x00)
                        return false;

                    var numberOfSections = BitConverter.ToInt16(buffer, peOffset + 4 + 2);
                    var optionalHeaderSize = BitConverter.ToInt16(buffer, peOffset + 4 + 16);
                    var sectionHeadersOffset = peOffset + 4 + 20 + optionalHeaderSize;

                    for (int i = 0; i < numberOfSections; i++)
                    {
                        var sectionOffset = sectionHeadersOffset + i * 40;
                        if (sectionOffset + 40 > 0x2000)
                            break;

                        var sectionName = Encoding.ASCII.GetString(buffer, sectionOffset, 8).TrimEnd('\0');
                        if (sectionName.ToLower().Equals(".drm"))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        internal static bool RemoveProtectionFromFile(string path, string decryptionKey)
        {
            var executablePath = Path.Combine(Path.GetTempPath(), "RemoveProtection.exe"); ;

            string executableDecryptedPath = path;
            var dotPos = path.LastIndexOf('.');
            if (dotPos == -1)
            {
                return false;
            }
            executableDecryptedPath = executableDecryptedPath.Insert(dotPos, "_decrypted");

            try
            {
                File.WriteAllBytes(executablePath, global::Forever.Properties.Resources.RemoveProtection);

                int exitCode = Helpers.RunProcess(executablePath, $"\"{path}\" \"{decryptionKey}\"");
                if (exitCode != 0)
                {
                    return false;
                }

                File.Move(executableDecryptedPath, path, true);

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                try
                {
                    File.Delete(executablePath);
                }
                catch { }
            }
        }
    }
}