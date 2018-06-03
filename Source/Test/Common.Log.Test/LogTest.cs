using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zhoubin.Infrastructure.Common.Log;
using Zhoubin.Infrastructure.Common.Log.Log4Net;

namespace Zhoubin.Infrastructure.Common.Test
{
    /// <summary>
    /// LogTest 的摘要说明
    /// </summary>
    [TestClass]
    public class LogTest
    {
        public LogTest()
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
        public static void MyClassInitialize(TestContext testContext) {

            CleanDir();
        }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
        }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        [TestInitialize()]
        public void MyTestInitialize()
        {
            //CleanDir(); 
        }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        [TestCleanup()]
        public void MyTestCleanup()
        {
           // CleanDir();
        }

        private static void CleanDir()
        {
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')+"\\Log4Net\\"))
            {
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\Log4Net\\", true);
            }
            if (Directory.Exists(".\\Nlog\\"))
            {
                Directory.Delete(".\\Nlog\\", true);
            }
        }
        //
        #endregion

        [TestMethod]
        public void LogFactoryShutDown()
        {
            var logger =
                LogFactory.GetLogger("NLog");
            logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
            logger =
               LogFactory.GetDefaultLogger();
            logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
            LogFactory.ShutDown();
        }
        [TestMethod]
        public void WriteLog4NetTest()
        {
            var list = new List<Task>();
            for (int i = 0; i < 2; i++)
                list.Add(new Task(Log4NetFunction));
            list.ForEach(p => p.Start());
            Task.WaitAll(list.ToArray());
            AssertLog4net();

        }

        [TestMethod]
        public void WriteLog4NetTest1()
        {
            Log4NetFunction();
            AssertLog4net();
        }

        private void AssertLog4net()
        {
            Assert.IsTrue(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\Log4Net\\", "*.txt").Any());
        }
        private void AssertNLog()
        {
            Assert.IsTrue(Directory.GetFiles(".\\Nlog\\", "*.log").Any());
        }

        private void Log4NetFunction()
        {
            var logger =
               LogFactory.GetDefaultLogger();
            for (var i = 0; i < 10; i++)
            {
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogExceptionEntity(new Exception("test", new ApplicationException("ApplicationException"))));

            }

            //logger.ShutDown();
        }

        [TestMethod]
        public void WriteNLog4Test()
        {
            var list = new List<Task>();
            for (int i = 0; i < 5; i++)
                list.Add(new Task(NLogFunction));
            list.ForEach(p => p.Start());
            Task.WaitAll(list.ToArray());
            AssertNLog();
        }

        private void NLogFunction()
        {
            var logger =
                LogFactory.GetLogger("NLog");
            for (var i = 0; i < 100; i++)
            {
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Info });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Debug });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Error });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Fatal });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Trace });
                logger.Write(new LogEntity { Title = "参加比选", Content = "四川日报招标比选参加比选成功。", Sevenrity = Sevenrity.Warn });
                logger.Write(new LogExceptionEntity(new Exception("test",new ApplicationException("ApplicationException"))));
            }
           
        }
    }
}
