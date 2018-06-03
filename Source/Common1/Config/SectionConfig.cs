using System;

namespace Zhoubin.Infrastructure.Net.Core.Common.Config
{
    public class SectionConfig
    {
        public SectionConfig(string name, string enittiesName)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "配置区域名称不能为空。");
            }
            Name = name;
            if (string.IsNullOrEmpty(enittiesName))
            {
                throw new ArgumentNullException("enittiesName", "配置区域实例名称不能为空。");
            }
            EntitiesName = enittiesName;
        }
        public string Name { get; }
        public string EntitiesName { get; }
    }
}