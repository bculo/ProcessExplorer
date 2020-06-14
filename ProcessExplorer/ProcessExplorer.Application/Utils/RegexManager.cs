using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProcessExplorer.Application.Utils
{
    public static class RegexManager
    {
        private static Dictionary<string, Regex> RegexDictionary { get; set; } = new Dictionary<string, Regex>();

        public static bool RegexExists(string key)
        {
            return RegexDictionary.ContainsKey(key.ToLower());
        }

        public static void AddRegex(string key, string search)
        {
            if (RegexExists(key))
                return;

            Regex newRegex = new Regex(search);
            RegexDictionary.Add(key.ToLower(), newRegex);
        }

        public static bool ValidRegex(string key, string input)
        {
            if (!RegexExists(key))
                return false;

            Regex reg = RegexDictionary[key.ToLower()];
            return reg.IsMatch(input);
        }

        public static void RemoveRegex(string key)
        {
            if (RegexExists(key))
                RegexDictionary.Remove(key.ToLower());
        }
    }
}
