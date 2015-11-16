using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
            var n = a.FullName;
            var n2 = a.GetName().Name;
        }

        private static Assembly aa()
        {
            return Assembly.GetCallingAssembly();
        }
    }
}
