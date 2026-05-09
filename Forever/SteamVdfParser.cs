using System.Text;

namespace Forever
{
    public class VdfParser
    {
        internal static List<string> GetSteamAppsFolders(string vdfContentPath)
        {
            string vfdContent = null;

            if (File.Exists(vdfContentPath))
            {
                try
                {
                    vfdContent = File.ReadAllText(vdfContentPath);
                }
                catch
                {
                    return null;
                }
            }

            if (string.IsNullOrEmpty(vfdContent))
            {
                return null;
            }

            var folders = new List<string>();

            var vdf = VdfParser.Parse(vfdContent);
            if (vdf.ContainsKey("libraryfolders"))
            {
                var vdfFolders = (Dictionary<string, object>)vdf["libraryfolders"];
                foreach (var vdfLibraryFolder in vdfFolders)
                {
                    var vdfLibraryFolderData = (Dictionary<string, object>)vdfLibraryFolder.Value;
                    if (vdfLibraryFolderData.ContainsKey("path"))
                    {
                        folders.Add(Path.Combine((string)vdfLibraryFolderData["path"], "steamapps"));
                    }
                }
            }

            return folders;
        }

        internal static bool GetAcfData(string acfPath, out string installDir, out string gameName)
        {
            installDir = null;
            gameName = null;

            string acfContent = null;

            if (File.Exists(acfPath))
            {
                try
                {
                    acfContent = File.ReadAllText(acfPath);
                }
                catch
                {
                    return false;
                }
            }

            if (string.IsNullOrEmpty(acfContent))
            {
                return false;
            }

            var acf = VdfParser.Parse(acfContent);
            if (acf.ContainsKey("AppState"))
            {
                var acfApp = (Dictionary<string, object>)acf["AppState"];

                if (acfApp.ContainsKey("installdir") && acfApp.ContainsKey("name"))
                {
                    installDir = (string)acfApp["installdir"];
                    installDir = Path.Combine(Path.Combine(Path.GetDirectoryName(acfPath), "common"), installDir);

                    gameName = (string)acfApp["name"];
                    return true;
                }
            }

            return false;
        }

        private readonly string _input;
        private int _position;

        public VdfParser(string input)
        {
            _input = input ?? throw new ArgumentNullException(nameof(input));
            _position = 0;
        }

        public Dictionary<string, object> Parse()
        {
            SkipWhitespace();
            return ParseObject();
        }

        private Dictionary<string, object> ParseObject()
        {
            var obj = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            while (_position < _input.Length)
            {
                SkipWhitespace();

                if (_position >= _input.Length || _input[_position] == '}')
                    break;

                string key = ReadString();

                SkipWhitespace();

                if (_input[_position] == '{')
                {
                    _position++;
                    var subObj = ParseObject();
                    _position++;
                    obj[key] = subObj;
                }
                else
                {
                    string value = ReadString();
                    obj[key] = value;
                }
            }

            return obj;
        }

        private string ReadString()
        {
            SkipWhitespace();

            if (_input[_position] == '"')
            {
                _position++;
                var sb = new StringBuilder();

                while (_position < _input.Length)
                {
                    char c = _input[_position++];
                    if (c == '"')
                        break;

                    if (c == '\\' && _position < _input.Length)
                    {
                        char next = _input[_position++];
                        sb.Append(next);
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
                return sb.ToString();
            }
            else
            {
                var sb = new StringBuilder();
                while (_position < _input.Length)
                {
                    char c = _input[_position];
                    if (char.IsWhiteSpace(c) || c == '{' || c == '}' || c == '"')
                        break;
                    sb.Append(c);
                    _position++;
                }
                return sb.ToString();
            }
        }

        private void SkipWhitespace()
        {
            while (_position < _input.Length)
            {
                char c = _input[_position];
                if (char.IsWhiteSpace(c))
                {
                    _position++;
                }
                else if (c == '/' && _position + 1 < _input.Length && _input[_position + 1] == '/')
                {
                    while (_position < _input.Length && _input[_position] != '\n')
                        _position++;
                }
                else
                {
                    break;
                }
            }
        }

        public static Dictionary<string, object> Parse(string vdfContent)
        {
            var parser = new VdfParser(vdfContent);
            return parser.Parse();
        }
    }

    public static class VdfExtensions
    {
        public static void PrintVdf(this Dictionary<string, object> dict, string indent = "")
        {
            foreach (var kvp in dict)
            {
                if (kvp.Value is Dictionary<string, object> subDict)
                {
                    Console.WriteLine($"{indent}\"{kvp.Key}\"");
                    Console.WriteLine($"{indent}{{");
                    subDict.PrintVdf(indent + "    ");
                    Console.WriteLine($"{indent}}}");
                }
                else
                {
                    Console.WriteLine($"{indent}\"{kvp.Key}\" \"{kvp.Value}\"");
                }
            }
        }
    }
}