using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace AmqNet40.Utils.Test
{
    [TestClass]
    public class TestSettings
    {
        [TestMethod]
        public void Test1()
        {
            var settings = SettingsFactory.Create(Assembly.GetExecutingAssembly().GetName().Name);
            Assert.IsTrue(!settings.Tokens.IsNullOrEmpty());
            Assert.IsTrue(!settings.FilePath.IsNullOrEmpty());

            var settings2 = SettingsFactory.Create(Assembly.GetExecutingAssembly().GetName().Name, "test.config");
            Assert.IsTrue(!settings2.Tokens.IsNullOrEmpty());
            Assert.IsTrue(!settings2.FilePath.IsNullOrEmpty());
        }
    }
}