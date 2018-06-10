using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zhoubin.Infrastructure.Common.MongoDb.Test
{
    [TestClass]
    public class UnitTestClearAll : TestBase
    {
        [TestMethod]
        public void TestInsert()
        {
            var acut = ClearAllObject();
            Assert.AreEqual(true, acut);
        }
    }
}