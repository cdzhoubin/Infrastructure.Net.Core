using System.Collections.Generic;
using System.Linq;

namespace Zhoubin.Infrastructure.Net.Core.Common.Config
{
    /// <summary>
    /// 配置区读取辅助类
    /// </summary>
    /// <typeparam name="T">要求<see cref="ConfigEntityBase"/>的子类</typeparam>
    class ConfigSectionEntity<T> where T : ConfigEntityBase<T>,new()
    {
        public ConfigSectionEntity()
        {
            Enities = new List<T>();
        }
        /// <summary>
        /// 读取配置实体列表
        /// </summary>
        public List<T> Enities { get; internal set; }

        /// <summary>
        /// 默认配置
        /// </summary>
        public T DefaultConfig {
            get
            {
                if (string.IsNullOrEmpty(DefaultConfigName))
                {
                    return default(T);
                }
                if (Enities.Any(p => p.Name == DefaultConfigName))
                {
                    return Enities.First(p => p.Name == DefaultConfigName);
                }
                return default(T);
            } }

        /// <summary>
        /// 默认配置名称
        /// </summary>
        internal string DefaultConfigName { get; set; }
    }
}