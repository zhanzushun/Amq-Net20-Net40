﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmqNet40.Utils
{
    public interface ISettings
    {
        string FilePath { get; }
        string Tokens { get; }

        string Get(string key);
        Dictionary<string, string> GetDict(string key);
        List<string> GetList(string key);
    }

    public interface ISettingsFactory
    {
        ISettings Create(string assemblyName, string fileName = null);
    }
}
