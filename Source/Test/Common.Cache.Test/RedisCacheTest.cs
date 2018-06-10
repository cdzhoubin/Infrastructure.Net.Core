using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zhoubin.Infrastructure.Common.Cache.Test
{
    [TestClass]
    public class RedisCacheTest : CacheTestBase
    {

        public RedisCacheTest()
            : base("RedisCacheProvider")
        {

        }

        [TestMethod]
        public void TestMethodMax2M()
        {
            byte[] buffer = new byte[1024 * 1024 * 2];
            var result = ObjProvider.Add("key2M", buffer);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestMethodMax100M()
        {
            byte[] buffer = new byte[1024 * 1024 * 10];
            var result = ObjProvider.Add("key100M", buffer);
            Assert.AreEqual(true, result);
        }
    }
}