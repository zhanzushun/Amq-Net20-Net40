using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmqNet40.Utils.Test
{
    [TestClass]
    public class TestSettings
    {
        private ISettings _settings;

        [TestInitialize]
        public void Setup()
        {
            _settings = new Settings("");
        }

        [TestMethod]
        public void Test1()
        {
            Assert.IsTrue(string.IsNullOrEmpty(_settings.TokenForKeyValue));
            Assert.IsTrue(string.IsNullOrEmpty(_settings.TokenForList));
            Assert.IsTrue(string.IsNullOrEmpty(_settings.TokenForDictKeyValue));
            Assert.IsTrue(string.IsNullOrEmpty(_settings.FilePath));
        }
    }
}