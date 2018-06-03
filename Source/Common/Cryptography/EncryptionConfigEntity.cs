using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Zhoubin.Infrastructure.Common.Config;

namespace Zhoubin.Infrastructure.Common.Cryptography
{
    /// <summary>
    /// 加密配置实体
    /// </summary>
    public sealed class EncryptionConfigEntity : ConfigEntityBase
    {
        /// <summary>
        /// 根据指定算法和Key创建加密配置
        /// </summary>
        /// <param name="algorithmProvider">算法</param>
        /// <param name="symmetricAlgorithm">同步异步算法</param>
        /// <param name="extentProperty">扩展数据</param>
        /// <returns>返回<see cref="EncryptionConfigEntity"/></returns>
        public static EncryptionConfigEntity CreateEncryptionConfigEntity(string algorithmProvider, bool symmetricAlgorithm, Dictionary<string, string> extentProperty)
        {
            var entity = new EncryptionConfigEntity
            {
                AlgorithmProvider = algorithmProvider,
                SymmetricAlgorithm = symmetricAlgorithm,
                ExtentProperty = extentProperty
            };
            return entity;
        }
        /// <summary>
        /// 配置构造函数
        /// </summary>
        public EncryptionConfigEntity()
        {
            ExtentProperty = new Dictionary<string, string>();
        }
        /// <summary>
        /// 日志处理器
        /// </summary>
        public string AlgorithmProvider { get; private set; }


        /// <summary>
        /// 扩展属性
        /// </summary>
        public Dictionary<string, string> ExtentProperty { get; private set; }

        /// <summary>
        /// 同步算法
        /// </summary>
        public bool SymmetricAlgorithm { get; private set; }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="node">结点</param>
        protected override void SetProperty(IConfigurationSection node)
        {
            switch (node.Key)
            {
                case "AlgorithmProvider":
                    AlgorithmProvider = node.Value;
                    break;
                case "SymmetricAlgorithm":
                    SymmetricAlgorithm = node.Value.ToLower() == "true";
                    break;
                default:
                    ExtentProperty.Add(node.Key, node.Value);
                    break;
            }
        }

        /// <summary>
        /// 解密，算法实现和扩展属性
        /// 暂时支持对称算法
        /// </summary>
        /// <param name="entity">待解密对象</param>
        /// <returns>返回解密后对象</returns>
        protected override void Decrypt()
        {
           base.Decrypt();

            AlgorithmProvider = Decrypt(AlgorithmProvider);
            foreach (var keyValuePair in ExtentProperty)
            {
                ExtentProperty[keyValuePair.Key] = Decrypt(keyValuePair.Value);
            }
        }
    }
}
