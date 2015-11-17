using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AmqNet40.Utils
{
    internal class Settings : ISettings
    {
        private readonly string _file;
        private readonly string _tokens = ":,|";
        private const string KeyTokens = "tokens";

        internal Settings(string assemblyName, string fileName)
        {
            _file = Path.Combine(DllDirectory, "Configurations", assemblyName, fileName);
            if (!File.Exists(_file))
                throw new ArgumentException(string.Format("File '{0}' doesn't exist", _file));     
            var overrideTokens = ReadOverrideTokens();
            if (!overrideTokens.IsNullOrEmpty())
                _tokens = overrideTokens;
        }

        internal string ReadOverrideTokens()
        {
            var lines = File.ReadAllLines(_file);
            if (lines.Length == 0)
                return null;

            var firstLine = lines.FirstOrDefault(x => x.Trim().Length > 0 
                && !x.Trim().StartsWith("#"));

            if (firstLine == null)
                return null;

            var firstLineKeyValue = firstLine
                .Split(new[] { ':' }, 2)
                .Select(x => x.Trim())
                .ToArray();

            if (firstLineKeyValue.Length == 2 && firstLineKeyValue[0] == KeyTokens 
                && firstLineKeyValue[1].Length >= 3)
                return firstLineKeyValue[1];
            return null;
        }

        private static string DllDirectory
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
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
                return _tokens;
            }
        }

        public string Get(string key)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetDict(string key)
        {
            throw new NotImplementedException();
        }

        public List<string> GetList(string key)
        {
            throw new NotImplementedException();
        }
    }

    internal class SettingsFactory : ISettingsFactory
    {
        public ISettings Create(string assemblyName, string fileName = null)
        {
            return CreateImpl(assemblyName, fileName);
        }

        internal static ISettings CreateImpl(string assemblyName, string fileName = null)
        {
            if (fileName.IsNullOrEmpty())
                fileName = "Settings.cfg";
            return new Settings(assemblyName, fileName);
        }
    }
}