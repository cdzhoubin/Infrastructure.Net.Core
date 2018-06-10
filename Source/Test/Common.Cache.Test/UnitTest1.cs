using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zhoubin.Infrastructure.Common.Cache.Test
{
    [TestClass]
    public class UnitTest1
    {

        [TestInitialize]
        public void Init()
        {
            DistCache.RemoveAll();
            DistCache.ChangeProvider("MemcachedCacheProvider");
        }
        [TestCleanup]
        public void Clear() { DistCache.RemoveAll(); }

        [TestMethod]
        public void TestMethodAdd()
        {
            var result = DistCache.Add("key1", "testServer");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestMethodGet()
        {
            DistCache.Add("key2", "testServer");
            var result = DistCache.Get<string>("key2");
            Assert.AreEqual("testServer", result);
        }

        [TestMethod]
        public void TestMethodAddSliding()
        {
            DistCache.ChangeProvider("MemcachedCacheSlidingProvider");
            string key = "TestMethodAddSliding";
            string value = Guid.NewGuid().ToString();
            var result = DistCache.AddForSliding(key, value, TimeSpan.FromSeconds(1));
            Assert.AreEqual(true, result);
            Thread.Sleep(600);
            var entity = DistCache.Get<string>(key);
            Assert.IsNotNull(entity);
            Assert.AreEqual(value, entity);
            Thread.Sleep(600);
            entity = DistCache.Get<string>(key);
            Assert.IsNotNull(entity);
            Assert.AreEqual(value, entity);
        }
    }

    [TestClass]
    public class UnitTestDistProviderName
    {
        private const string ProviderName = "HttpCacheProvider";

        [TestInitialize]
        public void Init()
        {
            DistCache.RemoveAll(ProviderName);
        }
        [TestCleanup]
        public void Clear() { DistCache.RemoveAll(ProviderName); }

        [TestMethod]
        public void TestMethodAdd()
        {
            var result = DistCache.Add("key1", "testServer", ProviderName);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestMethodGet()
        {
            DistCache.Add("key2", "testServer", ProviderName);
            var result = DistCache.Get<string>("key2", ProviderName);
            Assert.AreEqual("testServer", result);
        }

        [TestMethod]
        public void TestMethodAddSliding()
        {
            string key = "TestMethodAddSliding";
            string value = Guid.NewGuid().ToString();
            var result = DistCache.AddForSliding(key, value, TimeSpan.FromSeconds(1), ProviderName);
            Assert.AreEqual(true, result);
            Thread.Sleep(500);
            var entity = DistCache.Get<string>(key, ProviderName);
            Assert.IsNotNull(entity);
            Assert.AreEqual(value, entity);
            Thread.Sleep(600);
            entity = DistCache.Get<string>(key, ProviderName);
            Assert.IsNotNull(entity);
            Assert.AreEqual(value, entity);
        }
    }
}
