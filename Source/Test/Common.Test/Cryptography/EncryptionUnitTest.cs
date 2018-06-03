using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zhoubin.Infrastructure.Common.Cryptography;

namespace Zhoubin.Infrastructure.Common.Test.Cryptography
{
    [TestClass]
    public class EncryptionUnitTest
    {
        [TestMethod]
        public void TripleDesTest()
        {
            //qCie0wIiEMo11VpoRMmrAd8zzqG0nyYII57V9sq4p1BEd1iELKAXSQ==
            const string str = "测试数据加密，中英汇合acdefg";
            var result = Encryption.Encrypt(str, "TripleDES");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void RijndaelTest()
        {
            //Qfkfe201C/pmJTz/gyv3vGyaeYOnfKu1f1URRouCyWlzJjvpEGCeyX1JK2bln1NY
            const string str = "测试数据加密，中英汇合acdefg";
            var result = Encryption.Encrypt(str, "Rijndael");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void DesTest()
        {
            //OGUYlWsRx5oLjgIR+OT0K1BG2V5wf3Rbg1ddANmhLEWDeQfUojIqig==
            const string str = "测试数据加密，中英汇合acdefg";
            var result = Encryption.Encrypt(str, "DES");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Rc2Test()
        {
            //eq0EPanGM6JSrfH5fvtzCsxUl9BxNpm78CXyFgks4J22wxzU8/Y3BA==
            const string str = "测试数据加密，中英汇合acdefg";
            var result = Encryption.Encrypt(str, "RC2");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AesTest()
        {
            //2FYmJ9CZKwzGtYMNwkFD5yUbw2qh0ScoNJhSnCrULryaUoTGE+AB26mCt/6mAqE3
            const string str = "测试数据加密，123中英汇合acdefg";
            var result = Encryption.Encrypt(str, "Aes");
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        public void RsaTest()
        {
            //D44B125D2AFDED6E9A04CB1D0AD39378A56B947043BADBA89C32E577929E7E51076BF1E1BCF7537E
            const string str = "测试数据加密，中英汇合acdefg";
            
            byte[] key, iv, signdata;
            var result = Encryption.Encrypt(str, "RSA",out key,out iv,out signdata);
            Assert.IsNotNull(result);
            Assert.IsNotNull(key);
            Assert.IsNotNull(iv);
            Assert.IsNotNull(signdata);
        }
    }
}
