using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace AmqNet40.Utils.Test
{
    [TestClass]
    public class TestSettings
    {
        private ISettings _settings;

        [TestInitialize]
        public void Setup()
        {
            _settings = SettingsFactory.CreateImpl(Assembly.GetExecutingAssembly().GetName().Name);
        }

        [TestMethod]
        public void Test1()
        {
            Assert.IsTrue(!_settings.Tokens.IsNullOrEmpty());
            Assert.IsTrue(!_settings.FilePath.IsNullOrEmpty());
        }
    }
}