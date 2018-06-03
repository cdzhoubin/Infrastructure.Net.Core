using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zhoubin.Infrastructure.Common.Extent;

namespace Zhoubin.Infrastructure.Common.Test
{
    [TestClass]
    public class StringExtentUnitTest
    {
        [TestMethod]
        public void TestRandomcode1()
        {
            const int lenght = 5;
            var auct = string.Empty.RandomString();
            Assert.IsNotNull(auct);
            Assert.AreEqual(lenght, auct.Length);
            Assert.IsTrue(Regex.IsMatch(auct, "[abcdefghijklmnopqrstuvwxyz0123456789]{" + lenght + "}"));
        }

        [TestMethod]
        public void TestRandomcode2()
        {
            const int lenght = 6;
            var auct = string.Empty.RandomString(lenght);
            Assert.IsNotNull(auct);
            Assert.AreEqual(lenght, auct.Length);
            Assert.IsTrue(Regex.IsMatch(auct, "[abcdefghijklmnopqrstuvwxyz0123456789]{"+lenght+"}"));
        }

        [TestMethod]
        public void TestRandomcode3()
        {
            const int lenght = 5;
            var auct = string.Empty.RandomString(lenght,true);
            Assert.IsNotNull(auct);
            Assert.AreEqual(lenght, auct.Length);
            Assert.IsTrue(Regex.IsMatch(auct, "[\u4E00-\u9FA5]{" + lenght + "}"));
        }

        [TestMethod]
        public void TestRandomcode4()
        {
            const int lenght = 6;
            var auct = string.Empty.RandomString(lenght,true);
            Assert.IsNotNull(auct);
            Assert.AreEqual(lenght, auct.Length);
            Assert.IsTrue(Regex.IsMatch(auct, "[\u4E00-\u9FA5]{" + lenght + "}"));
        }

        

        [TestMethod]
        public void TestIdCard1()
        {
            const string expectMessag = "";
            const string idCard = "513001195801180016";
            string msg;
            var auct = idCard.IsIdCard(out msg);
            Assert.IsTrue(auct);
            Assert.AreEqual(expectMessag, msg);
        }

        [TestMethod]
        public void TestIdCard2()
        {
            const string expectMessag = "";
            const string idCard = "513001195801180016";
            var auct = idCard.IsIdCard();
            Assert.IsTrue(auct);
            Assert.AreEqual(expectMessag,"");
            
        }
        [TestMethod]
        public void TestIdCard3()
        {
            const string expectMessag = "身份证长度不符合要求";
            const string idCard = "51300119580118001";
            string msg;
            var auct = idCard.IsIdCard(out msg);
            Assert.IsFalse(auct);
            Assert.AreEqual(expectMessag, msg);
        }
        [TestMethod]
        public void TestIdCard4()
        {
            const string expectMessag = "身份证日期不符合要求";
            const string idCard = "51300119580230001X";
            string msg;
            var auct = idCard.IsIdCard(out msg);
            Assert.IsFalse(auct);
            Assert.AreEqual(expectMessag, msg);
        }

        [TestMethod]
        public void TestIdCard5()
        {
            const string expectMessag = "身份证验证错误";
            const string idCard = "51300119580118001X";
            string msg;
            var auct = idCard.IsIdCard(out msg);
            Assert.IsFalse(auct);
            Assert.AreEqual(expectMessag, msg);
        }


        [TestMethod]
        public void TestOrganizationCode1()
        {
            const string expectMessag = "";
            const string idCard = "12345678-8";
            string msg;
            var auct = idCard.IsOrganizationCode(out msg);
            Assert.IsTrue(auct);
            Assert.AreEqual(expectMessag, msg);
        }

        [TestMethod]
        public void TestOrganizationCode2()
        {
            const string expectMessag = "";
            const string idCard = "12345678-8";
            var auct = idCard.IsOrganizationCode();
            Assert.IsTrue(auct);
            Assert.AreEqual(expectMessag, "");

        }
        [TestMethod]
        public void TestOrganizationCode3()
        {
            const string expectMessag = "校验位不正确，请检查。";
            const string idCard = "12345678-x";
            string msg;
            var auct = idCard.IsOrganizationCode(out msg);
            Assert.IsFalse(auct);
            Assert.AreEqual(expectMessag, msg);
        }
        [TestMethod]
        public void TestOrganizationCode4()
        {
            const string expectMessag = "组织机构代码格式不正确，正确格式示例：E0000000-X";
            const string idCard = "12345678";
            string msg;
            var auct = idCard.IsOrganizationCode(out msg);
            Assert.IsFalse(auct);
            Assert.AreEqual(expectMessag, msg);
        }

        [TestMethod]
        public void TestOrganizationCode5()
        {
            const string expectMessag = "组织机构代码格式不正确，正确格式示例：E0000000-X";
            const string idCard = "12345678-xx";
            string msg;
            var auct = idCard.IsOrganizationCode(out msg);
            Assert.IsFalse(auct);
            Assert.AreEqual(expectMessag, msg);
        }
        [TestMethod]
        public void TestReadAppSetting()
        {
            const string key = "SmtpPasswordEncrypt";
            const string expectValue = "true";
            var auct = key.ReadAppSettingToString("false");
            Assert.AreEqual(expectValue, auct);
        }
        [TestMethod]
        public void TestReadAppSetting1()
        {
            const string key = "SmtpPasswordEncrypt1";
            const string expectValue = "false";
            var auct = key.ReadAppSettingToString("false");
            Assert.AreEqual(expectValue, auct);
        }
        [TestMethod]
        public void TestReadAppSetting2()
        {
            const string key = "SmtpPasswordEncrypt";
            const int expectValue = 50;
            var auct = key.ReadAppSettingInt(50);
            Assert.AreEqual(expectValue, auct);
        }
        [TestMethod]
        public void TestReadAppSetting3()
        {
            const string key = "IntKey";
            const int expectValue = 12;
            var auct = key.ReadAppSettingInt(10);
            Assert.AreEqual(expectValue, auct);
        }
        [TestMethod]
        public void TestReadAppSetting4()
        {
            const string key = "IntKey";
            const int expectValue = 15;
            var auct = key.ReadAppSettingInt(10,15,20);
            Assert.AreEqual(expectValue, auct);
        }
        [TestMethod]
        public void TestReadAppSetting5()
        {
            const string key = "IntKey";
            const int expectValue = 11;
            var auct = key.ReadAppSettingInt(10, 10, 11);
            Assert.AreEqual(expectValue, auct);
        }
        [TestMethod]
        public void TestReadAppSetting6()
        {
            const string key = "SmtpPasswordEncrypt";
            const decimal expectValue = (decimal) 12.9;
            var auct = key.ReadAppSettingToDecimal(new decimal(12.9));
            Assert.AreEqual(expectValue, auct);
        }
        [TestMethod]
        public void TestReadAppSetting7()
        {
            const string key = "IntDec";
            const decimal expectValue = (decimal)12.6;
            var auct = key.ReadAppSettingToDecimal((decimal)10.1);
            Assert.AreEqual(expectValue, auct);
        }
        [TestMethod]
        public void TestReadAppSetting8()
        {
            const string key = "IntDec";
            const decimal expectValue = (decimal)15.6;
            var auct = key.ReadAppSettingToDecimal((decimal)10.7, (decimal)15.6, (decimal)20.8);
            Assert.AreEqual(expectValue, auct);
        }
        [TestMethod]
        public void TestReadAppSetting9()
        {
            const string key = "IntDec";
            const decimal expectValue = (decimal)11.9;
            var auct = key.ReadAppSettingToDecimal((decimal)10.3, (decimal)10.1, (decimal)11.9);
            Assert.AreEqual(expectValue, auct);
        }

        [TestMethod]
        public void TestToSingular()
        {
            const string value = "days";
            const string expectValue = "day";
            var auct = value.ToSingular();
            Assert.AreEqual(expectValue, auct);
        }
        [TestMethod]
        public void TestToPlural()
        {
            const string value = "day";
            const string expectValue = "days";
            var auct = value.ToPlural();
            Assert.AreEqual(expectValue, auct);
        }
        [TestMethod]
        public void TestRunAppliction()
        {
            string appliction = "ping";
            bool result = appliction.Run("127.0.0.1");
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestRunAppliction_1()
        {
            string appliction = "ping";
            bool result = appliction.Run();
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestRunAppliction_2()
        {
            string appliction = "ping";
            var message = "";
            bool result = appliction.Run("",out message);
            Assert.AreEqual(true, result);
            Assert.IsTrue(message.Contains("用法: ping [-t] [-a] [-n count]"));
        }
        [TestMethod]
        public void TestRunAppliction_3()
        {
            string appliction = "ping";
            var message = "";
            bool result = appliction.Run("127.0.0.1", out message);
            Assert.AreEqual(true, result);
            Assert.IsTrue(message.Contains("正在 Ping 127.0.0.1 具有 32 字节的数据"));
            Assert.IsTrue(message.Contains("数据包: 已发送 = 4，已接收 = 4，丢失 = 0 (0% 丢失)"));
        }
        [TestMethod]
        public void TestGZip()
        {
            string source = "正在 Ping 127.0.0.1 具有 32 字节的数据";
            string expected = "LwAAAB+LCAAAAAAAAAt7tnbx0zkrFAIy89IVDI3M9QyA0FDhaev2Z3M6FYyNFJ6unf6iq+n5rJZnUzc8610HACD6VPAvAAAA";
            var result = source.GZipCompress();
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestUnGZip_1()
        {
            string source = "正在 Ping 127.0.0.1 具有 32 字节的数据";
            string expected = "LwAAAB+LCAAAAAAABAB7tnbx0zkrFAIy89IVDI3M9QyA0FDhaev2Z3M6FYyNFJ6unf6iq+n5rJZnUzc8610HACD6VPAvAAAA";
            var result = expected.GZipDecompress();
            Assert.AreEqual(source, result);
        }

        [TestMethod]
        public void TestUnGZip()
        {
            string source = "RwAAAB+LCAAAAAAABAABRwC4/+engeS6uuWkquepuuWFrOWPuCBTcGFjZVgg5pyd552A6YeN5aSN5L2/55So54Gr566t6L+I5Ye65LqG6YeN6KaB5LiA5q2lIbGpxEcAAAA=";
            string expected = "私人太空公司 SpaceX 朝着重复使用火箭迈出了重要一步";
            var result = source.GZipDecompress();
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestEnvironmentGet()
        {
            string source = null;
            string expected = null;
            var result = source.EnvironmentGet();
            Assert.AreEqual(expected, result);
        }
        
        [TestMethod]
        public void TestEnvironmentGet_2()
        {
            string source = "Path";
            bool expected = true;
            var result = source.EnvironmentGet();
            Assert.AreEqual(expected, result.ToLower().Contains("windows"));
        }
        [TestMethod]
        public void TestEnvironmentSet()
        {
            string source = "tempTest";
            bool expected = true;
            string value = Guid.NewGuid().ToString();
            var result = source.EnvironmentSet(value);
            Assert.AreEqual(expected, result);
            var result1 = source.EnvironmentGet();
            Assert.AreEqual(value,result1);
            result = source.EnvironmentSet(null);
            Assert.AreEqual(expected, result);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestEnvironmentSet_1()
        {
            string source = null;
            var result = source.EnvironmentSet("testvalue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEnvironmentSet_2()
        {
            string source = "";
            var result = source.EnvironmentSet("testvalue");
        }
       
        public void TestEnvironmentPathRemove()
        {
            var result = "testVar".EnvironmentPathRemove();
            Assert.AreEqual(true, result);
            Assert.IsFalse("PATH".StartsWith("testVal;"));
        }
        [TestMethod]
        public void TestEnvironmentExist()
        {
            string source = "Path";
            bool expected = true;
            var result = source.EnvironmentExist();
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestEnvironmentExist_1()
        {
            string source = "";
            bool expected = false;
            var result = source.EnvironmentExist();
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]

        public void TestEnvironmentExist_2()
        {
            string source = null;
            bool expected = false;
            var result = source.EnvironmentExist();
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestToDataTable()
        {
            string file = null;
            DataTable dt = file.CsvToDataTable(Encoding.Default);
            Assert.IsNull(dt);
        }
        [TestMethod]
        public void TestToDataTable_1()
        {
            string file = String.Format("{0}\\Resources\\{1}", AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'), "csvtest.csv");
            DataTable dt = file.CsvToDataTable(Encoding.Default);
            Assert.IsNotNull(dt);
            Assert.AreEqual(17,dt.Columns.Count);
            Assert.AreEqual(21,dt.Rows.Count);
        }
        [TestMethod]
        public void TestToDataTable_2()
        {
            string file = "D:\\" + Guid.NewGuid().ToString().Replace("-","")+".csv";
            DataTable dt = file.CsvToDataTable(Encoding.Default);
            Assert.IsNull(dt);
        }

        [TestMethod]
        public void TestToDataTable_3()
        {
            string file = File.ReadAllText(String.Format("{0}\\Resources\\{1}", AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'), "csvtest.csv"), Encoding.Default);
            DataTable dt = file.CsvContentToDataTable();
            Assert.IsNotNull(dt);
            Assert.AreEqual(17, dt.Columns.Count);
            Assert.AreEqual(21, dt.Rows.Count);
        }
    }
}
