using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Test
{
    [TestClass]
    public class TempTests
    {
        [TestMethod]
        public void SomeTests()
        {
            var a = aa();
            var n2 = a.GetName().Name;
            var a2 = string.Empty;
            var a3 = a2.Trim().StartsWith("#");
        }

        private static Assembly aa()
        {
            return Assembly.GetCallingAssembly();
        }
    }
}
