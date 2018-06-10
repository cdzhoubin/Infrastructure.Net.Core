using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zhoubin.Infrastructure.Common.MongoDb.Test
{
    [TestClass]
    public class TestClearall : TestBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            var result = ClearAllFile();
            Assert.AreEqual(true, result);
        }
    }
}