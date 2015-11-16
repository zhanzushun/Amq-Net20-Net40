using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmqNet40.Utils
{
    public interface ISettings
    {
        string FilePath { get; }
        string TokenForKeyValue { get; }
        string TokenForList { get; }
        string TokenForDictKeyValue { get; }

        string Get(string key);
        Dictionary<string, string> GetDict(string key);
        List<string> GetList(string key);
    }
}
