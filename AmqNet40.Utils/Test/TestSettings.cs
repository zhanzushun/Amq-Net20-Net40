using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace AmqNet40.Utils.Test
{
    [TestClass]
    public class TestSettings
    {
        [TestMethod]
        public void Test1()
        {
            var f1 = Settings.GetFilePath(Assembly.GetExecutingAssembly().GetName().Name,
                "test1.config");
            File.WriteAllText(f1,
@"
#comment out
Dictionary1:k1|,|v1,k2|v2|v3
List,1,2,3
List:1,2,,3,
Something:aaa
");
            var settings = SettingsFactory.Create(Assembly.GetExecutingAssembly().GetName().Name,
                "test1.config");

            Assert.AreEqual(settings.Tokens, ":,|");
            Assert.AreEqual(settings.FilePath, f1);
            Assert.AreEqual(settings.Get("aa"), null);
            Assert.AreEqual(settings.Get("Something"), "aaa");
            Assert.AreEqual(settings.GetDict("Dictionary"), null);
            Assert.AreEqual(settings.GetDict("Dictionary1").Count, 1);
            Assert.AreEqual(settings.GetDict("Dictionary1")["k2"], "v2|v3");
            Assert.AreEqual(settings.GetDictItem("Dictionary1","k1"), null);
            Assert.AreEqual(settings.Get("List,1,2,3"), null);
            Assert.AreEqual(settings.GetList("List").Count, 3);
            Assert.AreEqual(settings.GetList("List")[2], "3");

            File.WriteAllText(f1,
@"
tokens:abc
#this is a comment
dict1ak1cv1bk2cv2b
list1av1bv2bv3
");
            settings.Invalid();
            Assert.AreEqual(settings.Tokens, "abc");
            Assert.AreEqual(settings.FilePath, f1);
            Assert.AreEqual(settings.Get("aa"), null);
            Assert.AreEqual(settings.Get("tokens"), null);
            Assert.AreEqual(settings.GetDict("Dictionary"), null);
            Assert.AreEqual(settings.GetDict("dict1").Count, 2);
            Assert.AreEqual(settings.GetDictItem("dict1","k2"), "v2");
            Assert.AreEqual(settings.GetList("list1").Count, 3);
            Assert.AreEqual(settings.GetList("list1")[2], "v3");
        }
    }
}