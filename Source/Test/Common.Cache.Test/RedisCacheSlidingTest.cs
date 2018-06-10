using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zhoubin.Infrastructure.Common.Cache.Test
{
    [TestClass]
    public  class RedisCacheSlidingTest:CacheTestSlidingBase
    {
        public RedisCacheSlidingTest()
            : base("RedisCacheSlidingProvider")
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
