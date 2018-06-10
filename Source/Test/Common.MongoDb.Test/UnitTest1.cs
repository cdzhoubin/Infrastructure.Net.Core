using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zhoubin.Infrastructure.Common.MongoDb.Test
{
    [TestClass]
    public class UnitTest1 : TestBase
    {
        [TestInitialize]
        public void Init()
        {
            ClearAllObject();
        }
        [TestCleanup]
        public void Clear() { Init(); }
        [TestMethod]
        public void TestCreateFileStorage()
        {
            var storage = CreateFileStorage();
            Assert.AreNotEqual(null, storage);
        }

        [TestMethod]
        public void TestCreateObjectStorage()
        {
            var storage = CreateObjectStorage();
            Assert.AreNotEqual(null, storage);
        }
    }
}
