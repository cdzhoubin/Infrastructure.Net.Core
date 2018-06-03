using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Zhoubin.Infrastructure.Common.Tools;

namespace Zhoubin.Infrastructure.Common.Test
{
    /// <summary>
    /// IPReadTest 的摘要说明
    /// </summary>
    [TestClass]
    public class IPReadTest
    {
        public IPReadTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            QQWryLocator.SetDefaultData(File.ReadAllBytes("Resources\\qqwry.dat"));
        }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            var helper = LocatorFactory.GetLocator(Locator.QQWry);

            helper.Initiation();
            var ip = helper.Query("182.140.147.57");
            Assert.AreEqual("上海网宿科技股份有限公司电信CDN节点", ip.Local);
            Assert.AreEqual("四川省成都市", ip.Country);
            Assert.AreEqual("四川省",ip.Province);
            Assert.AreEqual("成都市", ip.City);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var helper = LocatorFactory.GetLocator(Locator.QQWry);

            helper.Initiation();
            var ip = helper.Query("118.122.117.55");
            Assert.AreEqual("电信", ip.Local);
            Assert.AreEqual("四川省成都市", ip.Country);
            Assert.AreEqual("四川省", ip.Province);
            Assert.AreEqual("成都市", ip.City);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var ip = IPLocation.Parse("北京市海淀区");
            Assert.AreEqual("北京市", ip[0]);
            Assert.AreEqual("海淀区", ip[1]);
        }
        [TestMethod]
        public void TestMethod6()
        {
            var helper = LocatorFactory.GetLocator(Locator.QQWry);

            helper.Initiation();
            var ip = helper.Query("1.4.4.255");
            //Assert.AreEqual("北龙中网(北京)科技有限责任公司", ip.Local);
            Assert.AreEqual("北京市海淀区", ip.Country);
            Assert.AreEqual("北京市", ip.Province);
            Assert.AreEqual("海淀区", ip.City);
        }
        [TestMethod]
        public void TestMethod3()
        {
            //1.15.255.255    北京市 北京北大方正宽带网络科技有限公司
            var helper = LocatorFactory.GetLocator(Locator.QQWry);

            helper.Initiation();
            var ip = helper.Query("1.15.255.255");
            Assert.AreEqual("北京北大方正宽带网络科技有限公司", ip.Local);
            Assert.AreEqual("北京市", ip.Country);
            Assert.AreEqual("北京市", ip.Province);
            Assert.AreEqual("北京市", ip.City);
        }

        [TestMethod]
        public void TestMethod4()
        {
            //27.98.233.255   西藏拉萨市 联通
            var helper = LocatorFactory.GetLocator(Locator.QQWry);

            helper.Initiation();
            var ip = helper.Query("27.98.233.255");
            Assert.AreEqual("联通", ip.Local);
            Assert.AreEqual("西藏拉萨市", ip.Country);
            Assert.AreEqual("西藏", ip.Province);
            Assert.AreEqual("拉萨市", ip.City);
        }
    }
}
