using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AmqNet40.Utils
{
    public class Settings : ISettings
    {
        public Settings(string assemblyName)
        {
            //Assembly.GetExecutingAssembly()
        }

        public string FilePath
        {
            get
            {
                //Assembly.GetCallingAssembly()
                throw new NotImplementedException();
                
            }
        }

        public string TokenForDictKeyValue
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string TokenForKeyValue
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string TokenForList
        {
            get
            {
                throw new NotImplementedException();
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
}
