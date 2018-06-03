using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zhoubin.Infrastructure.Common.Cryptography;

namespace Zhoubin.Infrastructure.Common.Test.Cryptography
{
    [TestClass]
    public class DecryptionUnitTest
    {
        [TestMethod]
        public void TripleDesTest()
        {
            const string source = "qCie0wIiEMo11VpoRMmrAd8zzqG0nyYII57V9sq4p1BEd1iELKAXSQ==";
            const string str = "测试数据加密，中英汇合acdefg";
            var result = Decryption.Decrypt(source, "TripleDES");
            Assert.AreEqual(str,result);
        }
        [TestMethod]
        public void RijndaelTest()
        {
            const string source = "Qfkfe201C/pmJTz/gyv3vGyaeYOnfKu1f1URRouCyWlzJjvpEGCeyX1JK2bln1NY";
            const string str = "测试数据加密，中英汇合acdefg";
            var result = Decryption.Decrypt(source, "Rijndael");
            Assert.AreEqual(str, result);
        }
        [TestMethod]
        public void DesTest()
        {
            const string source = "OGUYlWsRx5oLjgIR+OT0K1BG2V5wf3Rbg1ddANmhLEWDeQfUojIqig==";
            const string str = "测试数据加密，中英汇合acdefg";
            var result = Decryption.Decrypt(source, "DES");
            Assert.AreEqual(str, result);
        }
        [TestMethod]
        public void AesTest()
        {
            const string source = "2FYmJ9CZKwzGtYMNwkFD5yUbw2qh0ScoNJhSnCrULryaUoTGE+AB26mCt/6mAqE3";
            const string str = "测试数据加密，123中英汇合acdefg";
            var result = Decryption.Decrypt(source, "Aes");
            Assert.AreEqual(str, result);
        }
        [TestMethod]
        public void Rc2Test()
        {
            const string source = "eq0EPanGM6JSrfH5fvtzCsxUl9BxNpm78CXyFgks4J22wxzU8/Y3BA==";
            const string str = "测试数据加密，中英汇合acdefg";
            var result = Decryption.Decrypt(source, "RC2");
            Assert.AreEqual(str, result);
        }

        //[TestMethod]
        public void RsaTest()
        {
            //KhGI3xDKboMU6ddLemIXt3Y5WnCuDXedXUzr5ZAzDY5/rSrGQNG/qQ==
            const string str = "SLuw1TrMgrHgCxP6SdARgqDx46UnSgraT92CAdeAM+fnkUd0WAx2ZA==";
            const string ivstr = "PLrPmAfV1AlGjd2WBEaFHn3CtJNi51a0we2hz2XNoWLxUDxURiebsaLYuOey1/rxQo6wM+KDpCCPhCKRkRTYoj9CRT321wmK7clVnR0RgAwtlI5ZoKfx/gaSZRQHzHCVYi9SIjMWC5m51zG5huR9VEQJwQ1wUkGqA3WtLzr30ahEJJaA15jVFNTtPI8WlkZpv5Lk5jsNsNXRkR0mZXSNpT4WNalOHpgnKPFmEImGtlnTTrgz8i0/cI9MgyaAK6+s59E6ZDoXCW/p+e/wAFpbQnRy4VjHvT8sQKTm2oUdfhqdznpw9Mw2o2VavyKJAY4u0e1WaObdN07i4nzWQwNloQ==";
            const string keystr = "ULsMvA1DVz5Tym6tlHB/DqXzordptblEpvtcZ0Z5jmKlUquqydIHlnYd87ZgKQvGVs6z8N2KTA1WA38SX7WUmKZPazySZyDvQ1V62rrlDp3nWw95f4GMIqEl4godPeeXB7HZ4/qAz9s8X1xPLpQRvPA11bsuXIhcaS9+jl1Tsi8z+voemBmtR4QV3ex34Sc62EHdZoVKvKQ3WEDS76+BgfcE98u2H7/HIbJr+LUrL9Cu82gokasjmWZsfTDFL3DU+ZJWhffL5puqk2it2wzrmLU6QgkOW3EOGLoS6p81DV24TfCTT21LmcZ+aVn2/B5OIWw3laIFCs+ZugYskYJlEg==";
            const string signdatastr = "fV31Zq3K4ok5dpX8631v/bI59np6nxDd224oihKKNz10FT2aCiXbKDeOEAOD7RgiKs/dIJ9AXJulVXpPk7FacxWyglqn6PHa8sjMWt/qGBzF5R8RqsoJuPQW5gHTSgtIsPXsf+S3lMEFme9+/JvoWHUXfQ14VrAB7DMLjrg8kesVnun7LCU6TutFMhle8n942x7+TgyBFqzsWwAe3L8GPvsV+Lvi9jIc35BgtxzG1b9/3QMBJ3EgeD1oRDDaf0GKfBkq3JYitEKJIWTaAWHfku1zPeqTWN7wDqBrqrLkq2qbeO0gE96Feo5jZJhSh2rM41Kmpqd7ALSrgnlHJgJkMQ==";

            var key = Convert.FromBase64String(keystr);
            var iv = Convert.FromBase64String(ivstr);
            var signdata = Convert.FromBase64String(signdatastr);

            var result1 = Decryption.Decrypt(str, "RSADecry", key, iv, signdata);

            Assert.AreEqual("测试数据加密，中英汇合acdefg", result1);
        }
        internal static string ByteToHex(byte[] encryptedData)
        {
            return Convert.ToBase64String(encryptedData);
        }

    }
}