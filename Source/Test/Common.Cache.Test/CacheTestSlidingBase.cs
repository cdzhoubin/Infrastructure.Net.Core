using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Zhoubin.Infrastructure.Common.Cache.Test
{
    public abstract class CacheTestSlidingBase : CacheTestBase
    {
        public CacheTestSlidingBase(string providerName) : base(providerName)
        {
        }
        [TestMethod]
        public new void TestMethodAdd_5()
        {
            string key = "key1_5";
            string value = "testServer";
            var result = ObjProvider.AddForSliding(key, value, TimeSpan.FromMilliseconds(2500));
            Assert.AreEqual(true, result);
            Thread.Sleep(1500);
            var result1 = ObjProvider.Get(key);
            Assert.IsNotNull(result1);
            Assert.AreEqual(value, result1.ToString());
            Thread.Sleep(1500);
            result1 = ObjProvider.Get(key);
            Assert.IsNotNull(result1);
            Assert.AreEqual(value, result1.ToString());
        }

        [TestMethod]
        public new void TestMethodAdd_6()
        {
            string key = "key1_6";
            TestEntity value = new TestEntity
            {
                Name = "1",
                Age = 12,
            };
            var result = ObjProvider.AddForSliding(key, value, TimeSpan.FromMilliseconds(2500));
            Assert.AreEqual(true, result);
            Thread.Sleep(1500);
            var result1 = ObjProvider.Get<TestEntity>(key);
            Assert.IsNotNull(result1);
            Assert.IsTrue(value.Equals(result1));
            Thread.Sleep(1500);
            result1 = ObjProvider.Get<TestEntity>(key);
            Assert.IsNotNull(result1);
            Assert.IsTrue(value.Equals(result1));
        }
    }
    [Serializable]
    public class TestEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var obj1 = obj as TestEntity;
            if (obj1 == null)
            {
                return false;
            }
            return this.Name == obj1.Name && this.Age == obj1.Age;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
