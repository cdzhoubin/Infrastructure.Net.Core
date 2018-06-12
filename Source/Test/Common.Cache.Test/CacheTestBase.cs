using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zhoubin.Infrastructure.Common.Cache.Test
{
    [TestClass]
    public abstract class CacheTestBase
    {
        protected CacheProvider ObjProvider;
        private string _providerName;

        protected CacheTestBase(string providerName)
        {
            _providerName = providerName;
            ObjProvider = DistCache.GetCacheProvider(providerName);
        }



        [TestInitialize]
        public void Init()
        {
            ObjProvider.RemoveAll();
        }

        [TestCleanup]
        public void Clear()
        {
            ObjProvider.RemoveAll();
        }

        [TestMethod]
        public void TestMethodAdd()
        {
            var result = ObjProvider.Add("key1", "testServer");
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestMethodAdd_1()
        {
            string key = "key1_1";
            string value = "testServer";
            var result = ObjProvider.Add(key, value, true);
            Assert.AreEqual(true, result);
            var result1 = ObjProvider.Get(key);
            Assert.IsNotNull(result1);
            Assert.AreEqual(value, result1.ToString());
            var sleepTime = (int)ObjProvider.DefaultExpireTime * 1000;
            Thread.Sleep(sleepTime+1000);
            result1 = ObjProvider.Get(key);
            Assert.IsNull(result1);
        }
        [TestMethod]
        public void TestMethodAdd_2()
        {
            string key = "key1_2";
            string value = "testServer_12";
            var result = ObjProvider.Add(key, value, false);
            Assert.AreEqual(true, result);
            var result1 = ObjProvider.Get(key);
            Assert.IsNotNull(result1);
            Assert.AreEqual(value, result1.ToString());
            Thread.Sleep((int)ObjProvider.DefaultExpireTime * 1000);
            result1 = ObjProvider.Get(key);
            Assert.IsNotNull(result1);
            Assert.AreEqual(value, result1.ToString());
        }
        [TestMethod]
        public void TestMethodAdd_3()
        {
            string key = "key1_3";
            string value = "testServer";
            var result = ObjProvider.Add(key, value, 1000);
            Assert.AreEqual(true, result);
            Thread.Sleep(405);
            var result1 = ObjProvider.Get(key);
            Assert.IsNotNull(result1);
            Assert.AreEqual(value, result1.ToString());
            Thread.Sleep(2000);
            var  result2 = ObjProvider.Get(key);
            Assert.IsNull(result2);
        }
        [TestMethod]
        public void TestMethodAdd_4()
        {
            string key = "key1_4";
            string value = "testServer";
            var result = ObjProvider.Add(key, value, TimeSpan.FromMilliseconds(1000));
            Assert.AreEqual(true, result);
            var result1 = ObjProvider.Get(key);
            Assert.IsNotNull(result1);
            Assert.AreEqual(value, result1.ToString());
            Thread.Sleep(300);
            result1 = ObjProvider.Get(key);
            Assert.IsNotNull(result1);
            Assert.AreEqual(value, result1.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestMethodAdd_5()
        {
            string key = "key1_5";
            string value = "testServer";
            var result = ObjProvider.AddForSliding(key, value, TimeSpan.FromMilliseconds(1000));
            Assert.AreEqual(true, result);
            Thread.Sleep(600);
            var result1 = ObjProvider.Get(key);
            Assert.IsNotNull(result1);
            Assert.AreEqual(value, result1.ToString());
            Thread.Sleep(600);
            result1 = ObjProvider.Get(key);
            Assert.IsNotNull(result1);
            Assert.AreEqual(value, result1.ToString());
        }

        [TestMethod]

        [ExpectedException(typeof(NotImplementedException))]
        public void TestMethodAdd_6()
        {
            string key = "key1_6";
            TestEntity value = new TestEntity
            {
                Name = "1",
                Age = 12,
            };
            var result = ObjProvider.AddForSliding(key, value, TimeSpan.FromMilliseconds(1000));
            Assert.AreEqual(true, result);
            Thread.Sleep(600);
            var result1 = ObjProvider.Get<TestEntity>(key);
            Assert.IsNotNull(result1);
            Assert.IsTrue(value.Equals( result1));
            result1 = ObjProvider.Get<TestEntity>(key);
            Assert.IsNotNull(result1);
            Assert.IsTrue(value.Equals(result1));
            Thread.Sleep(600);
            result1 = ObjProvider.Get<TestEntity>(key);
            Assert.IsNotNull(result1);
            Assert.IsTrue(value.Equals(result1));
        }
        [TestMethod]
        public void TestMethodGet()
        {
            ObjProvider.Add("key2", "testServer");
            var result = ObjProvider.Get<string>("key2");
            Assert.AreEqual("testServer", result);
        }

        [TestMethod]
        public void TestMethodValid()
        {
            int second = 2;
            ObjProvider.Add("key2_Valid", "testServer_Valid", DateTime.Now.AddSeconds(second));
            var result = ObjProvider.Get("key2_Valid");
            Assert.AreEqual("testServer_Valid", result);

            Thread.Sleep(second * 1000 *2);
            
            result = ObjProvider.Get<string>("key2_Valid");
            
            Assert.IsNull(result);
        }

    }

    
}