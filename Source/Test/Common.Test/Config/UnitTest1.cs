using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zhoubin.Infrastructure.Common.Config;

namespace Zhoubin.Infrastructure.Common.Test.Config
{
    [TestClass]
    public class ConfigTest
    {
         static string targetAppPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + "TestApp.config";
        [TestMethod]
        public void TestReadDefaultConfig()
        {
            var helper = new ConfigHelper<Entity>("Entities");
            Assert.IsNotNull(helper);
            Assert.AreEqual(2,helper.Count);
        }

        [TestMethod]
        public void TestReadDefaultConfigReadData()
        {
            var helper = new ConfigHelper<Entity>("Entities");
            Assert.AreEqual("信息设置2", helper["Name2"].Propety);
        }

        

        [TestMethod]
        public void TestReadConfigDefault()
        {
            var helper = new ConfigHelper<Entity>("Entities");
            Assert.IsNotNull(helper.DefaultConfig);
            Assert.AreEqual("Name2", helper.DefaultConfig.Name);

        }
        [TestMethod]
        public void TestReadConfigDefault_1()
        {
            var helper = new ConfigHelper<Entity>("Entities");
            var item = helper["Name1"];
            Assert.IsNotNull(item);
            Assert.AreEqual("Name1", item.Name);
            Assert.AreEqual(1, item.ExtentProperty.Count);
            Assert.AreEqual("key-1", item.ExtentProperty.Keys.First());
            Assert.AreEqual("Value-1", item.ExtentProperty.Values.First());

        }
    }
}
