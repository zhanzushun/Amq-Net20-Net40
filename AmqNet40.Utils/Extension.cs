using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmqNet40.Utils
{
    public static class Extension
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
        public static string[] SplitTrim(this string s, char token, int maxSubStringCount)
        {
            return s.Split(new char[] { token }, maxSubStringCount)
                .Select(x => x.Trim()).Where(x => !x.IsNullOrEmpty()).ToArray();
        }
        public static string[] SplitTrim(this string s, char token)
        {
            return s.Split(new char[] { token })
                .Select(x => x.Trim()).Where(x => !x.IsNullOrEmpty()).ToArray();
        }
        public static void AddNonExist<T>(this Dictionary<string, T> dict, string key, T value)
        {
            if (!dict.ContainsKey(key))
                dict[key] = value;
        }
        public static T GetOrNull<T>(this Dictionary<string, T> dict, string key) where T : class
        {
            if (dict.ContainsKey(key))
                return dict[key];
            return null;
        }
    }
}
