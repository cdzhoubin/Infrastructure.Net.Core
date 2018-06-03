using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zhoubin.Infrastructure.Net.Core.Common.Config;
using Zhoubin.Infrastructure.Net.Core.Common.Cryptography;

namespace Common.Test.Cryptography
{
    [TestClass]
    public class EncryptionConfigTest
    {
        [TestMethod]
        public void TestConfigRead()
        {
           var helper = new ConfigHelp<EncryptionConfigEntity>(new SectionConfig("EncryptionService", "Entities"));
            Assert.AreEqual("TripleDES", helper.DefaultConfig.Name);
            Assert.AreEqual(5,helper.Count);
        }
    }
}
