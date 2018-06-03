using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Zhoubin.Infrastructure.Common.Cryptography;
using Zhoubin.Infrastructure.Common.Extent;

namespace Zhoubin.Infrastructure.Common.Config
{


    /// <summary>
    /// 基本配置对象基类
    /// </summary>
    public class ConfigEntityBase
    {
        #region 配置自动解密相关
        private static EncryptionConfigEntity _configEntity;
        private static readonly Hashtable HtSyc = new Hashtable();

        private static string[] GenKeyContent()
        {
            var provider = new AesCryptoServiceProvider();
            provider.GenerateIV();
            provider.GenerateKey();
            return new[]
            {
                provider.GetType().AssemblyQualifiedName,
                Decryption.BytesToBase64(provider.IV),
                Decryption.BytesToBase64(provider.Key)
            };
        }

        /// <summary>
        /// 获取默认加密配置
        /// 如果配置文件keyfile.txt不存在自动生成一个
        /// </summary>
        public static EncryptionConfigEntity DefaultEncryptionConfigEntity
        {
            get
            {
                if (_configEntity == null)
                    lock (HtSyc.SyncRoot)
                    {
                        if (_configEntity == null)
                        {
                            _configEntity = LoadConfigEntity();
                        }
                    }
                return _configEntity;
            }
        }


        /// <summary>
        /// 对关键数据进行加密进，重载此方法进行解密
        /// </summary>
        /// <param name="entity">待解密对象</param>
        /// <returns>返回解密后对象</returns>
        internal protected virtual void Decrypt()
        {
        }

        private static readonly string[] KeyInfo = new[]
                {
                    "System.Security.Cryptography.AesCryptoServiceProvider, System.Core,Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
                    "c2N6YmJ4LmNvbSFAIyQlXg==",
                    "c2N6YmJ4LmNvbXNjemJieC5jb21zY3piYnguY29tQDE="
                };
        private static EncryptionConfigEntity LoadConfigEntity()
        {
            string[] keyInfo = null;
            if (!EnableAutoGenKey)
            {
                keyInfo = KeyInfo;
            }
            else
            {
                var filePath = string.Format("{0}{1}{2}", AppContext.BaseDirectory.TrimEnd(Path.PathSeparator), Path.PathSeparator, "keyfile.txt");

                var isFind = false;
                try
                {
                    if (File.Exists(filePath))
                    {
                        var str = File.ReadAllText(filePath, Encoding.UTF8);
                        keyInfo = Decryption.Decrypt(str, EncryptionConfigEntity.CreateEncryptionConfigEntity(KeyInfo[0]
                            , true, new Dictionary<string, string> { { "Key", KeyInfo[2] }, { "IV", KeyInfo[1] } }))
                            .DeserializeObject<string[]>();

                        if (keyInfo.Length == 3) //如果key文件格式不正确，自动生成新key
                        {
                            isFind = true;
                        }
                    }

                }
                catch (Exception) //
                {
                    isFind = false;
                    //Log.LogFactory.GetDefaultLogger().Write("加载文件出错,自动生成新文件，错误信息：" + exception);

                }

                if (isFind == false) //加载key失败使用默认key
                {
                    keyInfo = GenKeyContent();
                    var str = Encryption.Encrypt(keyInfo.SerializeObject(), EncryptionConfigEntity.CreateEncryptionConfigEntity(KeyInfo[0], true, new Dictionary<string, string> { { "Key", KeyInfo[2] }, { "IV", KeyInfo[1] } }));
                    File.WriteAllText(filePath, str, Encoding.UTF8);
                }
            }

            return EncryptionConfigEntity.CreateEncryptionConfigEntity(keyInfo[0], true, new Dictionary<string, string> { { "Key", keyInfo[2] }, { "IV", keyInfo[1] } });
        }


        internal static bool EnableAutoGenKey
        {
            get; set;
        }
        #endregion

        /// <summary>
        /// 配置构造函数
        /// </summary>
        public ConfigEntityBase()
        {
            EnableCryptography = false;
        }
        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>加密后数据</returns>
        protected static string Decrypt(string data)
        {
            return Decryption.Decrypt(data, DefaultEncryptionConfigEntity);
        }
        private string _name;
        private Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();

        protected dynamic GetValue<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            return default(T);
        }
        protected void SetValue(string key, dynamic value)
        {
            if (_lock)
            {
                throw new InfrastructureException("配置对象运行时，不能修改。");
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.Add(key, value);
            }
        }
        private bool _lock;
        internal void LockDataSet()
        {
            _lock = true;
        }
        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get { return GetValue<string>("Name"); } set { SetValue("Name", value); } }

        /// <summary>
        /// 启用数据加密
        /// </summary>
        public bool EnableCryptography { get { return GetValue<bool>("EnableCryptography"); } set { SetValue("EnableCryptography", value); } }

        /// <summary>
        /// 默认配置
        /// </summary>
        public bool Default { get { return GetValue<bool>("Default"); } set { SetValue("Default", value); } }
    }
}
