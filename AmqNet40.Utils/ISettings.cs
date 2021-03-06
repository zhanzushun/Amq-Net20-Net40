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
        string GetDictItem(string key, string key2);
        List<string> GetList(string key);
        void Invalid();
    }
}
