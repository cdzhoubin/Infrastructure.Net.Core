using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zhoubin.Infrastructure.Common.Test
{
    [TestClass]
    public class TestPageInfo
    {
        [TestMethod]
        public void TestMethod1()
        {
            PageInfo<string> test = new PageInfo<string>(1, 2, 10, new System.Collections.Generic.List<string>());
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(test);
            var ob = Newtonsoft.Json.JsonConvert.DeserializeObject<PageInfo<string>>(str);
            Assert.AreEqual(1, ob.Index);
        }
    }
}
