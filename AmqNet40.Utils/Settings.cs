using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AmqNet40.Utils
{
    internal class Settings : ISettings
    {
        private readonly string _file;

        private const string Default_tokens = ":,|";
        private string _tokens = Default_tokens;
        private readonly Dictionary<string, Dictionary<string, string>> _dictOfDicts;
        private readonly Dictionary<string, List<string>> _dictOfLists;
        private readonly Dictionary<string, string> _dict;
        private bool _loaded = false;

        private const string KEY_TOKENS = "tokens";

        internal Settings(string assemblyName, string fileName)
        {
            _file = GetFilePath(assemblyName, fileName);

            if (!File.Exists(_file))
                throw new ArgumentException(string.Format("File '{0}' doesn't exist", _file));

            _dictOfDicts = new Dictionary<string, Dictionary<string, string>>();
            _dictOfLists = new Dictionary<string, List<string>>();
            _dict = new Dictionary<string, string>();

            LoadFile();
        }

        public static string GetFilePath(string assemblyName, string fileName)
        {
            return Path.Combine(Utils.AssemblyDirectory, "Configurations", assemblyName,
                fileName);
        }

        private void LoadFile()
        {
            ParseFile();
            _loaded = true;
        }

        private void ParseFile()
        {
            var originalLines = File.ReadAllLines(_file)
                .Where(x => x.Trim().Length > 0 && !x.Trim().StartsWith("#"))
                .Select(x => x.Trim())
                .ToArray();

            if (originalLines.Length == 0)
                return;

            var firstLineKeyValue = GetPairOrNull(originalLines[0]);
            if (firstLineKeyValue != null && firstLineKeyValue[0] == KEY_TOKENS)
                _tokens = firstLineKeyValue[1];

            foreach (var line in originalLines)
            {
                if (line.StartsWith(KEY_TOKENS + Default_tokens[0]))
                    continue;

                var pair = GetPairOrNull(line);
                if (pair == null)
                    continue;

                var list = pair[1].SplitTrim(_tokens[1]);
                if (list.Length == 0)
                    continue;

                if (list[0].Contains(_tokens[2]))
                {
                    _dictOfDicts.AddNonExist(pair[0], list
                        .Select(x => x.SplitTrim(_tokens[2], 2))
                        .Where(arr => arr.Length == 2)
                        .GroupBy(keyValue => keyValue[0])
                        .ToDictionary(group => group.Key, group => group.First()[1]));
                    continue;
                }

                if (list.Length == 1)
                    _dict.AddNonExist(pair[0], pair[1]);
                else
                    _dictOfLists.AddNonExist(pair[0], list.ToList());
            }
        }

        private string[] GetPairOrNull(string line)
        {
            var keyValue = line.SplitTrim(_tokens[0], 2);
            if (keyValue.Length != 2)
                return null;
            return keyValue;
        }

        private object _lock = new object();

        public void Invalid()
        {
            lock (_lock)
            {
                _tokens = Default_tokens;
                _dict.Clear();
                _dictOfDicts.Clear();
                _dictOfLists.Clear();
                _loaded = false;
            }
        }

        public string FilePath
        {
            get
            {
                return _file;
            }
        }

        public string Tokens
        {
            get
            {
                EnsureLoaded();
                return _tokens;
            }
        }

        private void EnsureLoaded()
        {
            if (!_loaded)
                lock (_lock)
                    if (!_loaded)
                        LoadFile();
        }

        public string Get(string key)
        {
            EnsureLoaded();
            lock (_lock)
                return _dict.GetOrNull(key);
        }

        public Dictionary<string, string> GetDict(string key)
        {
            EnsureLoaded();
            lock (_lock)
                return _dictOfDicts.GetOrNull(key);
        }

        public string GetDictItem(string key, string key2)
        {
            EnsureLoaded();
            lock (_lock)
            {
                if (_dictOfDicts.GetOrNull(key) == null)
                    return null;
                return _dictOfDicts.GetOrNull(key).GetOrNull(key2);
            }
        }

        public List<string> GetList(string key)
        {
            EnsureLoaded();
            lock (_lock)
                return _dictOfLists.GetOrNull(key);
        }
    }

    public static class SettingsFactory
    {
        public static ISettings Create(string assemblyName, string fileName = null)
        {
            if (fileName.IsNullOrEmpty())
                fileName = "Settings.cfg";
            return new Settings(assemblyName, fileName);
        }
    }
}