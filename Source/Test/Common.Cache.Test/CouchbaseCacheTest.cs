using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zhoubin.Infrastructure.Common.Cache.Test
{
    [TestClass]
    public class CouchbaseCacheTest : CacheTestBase
    {

        public CouchbaseCacheTest()
            : base("MemcachedCacheProvider")
        {

        }

        [TestMethod]
        public void TestMethodMaxLimit1M()//membercache最大不超过1M
        {
            byte[] buffer = new byte[1024 * 1023];
            var result = ObjProvider.Add("key2M", buffer);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestMethodMax12M()
        {
            byte[] buffer = new byte[1024 * 1024 * 14];
            var result = ObjProvider.Add("key12M", buffer);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestMethodMaxLimit05M()//membercache最大不超过1M
        {
            byte[] buffer = new byte[1024 * 512];
            for (int i = 0; i < 10; i++)
            { Assert.AreEqual(true, ObjProvider.Add("key2M" + i, buffer, 2000 * i + 2000)); }

        }
    }
    [TestClass]
    public class CouchbaseCacheSlidingTest : CacheTestSlidingBase
    {

        public CouchbaseCacheSlidingTest()
            : base("MemcachedCacheSlidingProvider")
        {

        }

        [TestMethod]
        public void TestMethodMaxLimit1M()//membercache最大不超过1M
        {
            byte[] buffer = new byte[1024 * 1023];
            var result = ObjProvider.Add("key2M", buffer);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestMethodMax20M()
        {
            byte[] buffer = new byte[1024 * 1024 * 14];
            var result = ObjProvider.Add("key20M", buffer);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestMethodMaxLimit05M()//membercache最大不超过1M
        {
            byte[] buffer = new byte[1024 * 512];
            for (int i = 0; i < 10; i++)
            { Assert.AreEqual(true, ObjProvider.Add("key2M" + i, buffer, 2000 * i + 2000)); }

        }
    }
}