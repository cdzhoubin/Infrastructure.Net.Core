using System;

namespace Zhoubin.Infrastructure.Net.Core.Common.Config
{
    public class SectionConfig
    {
        public SectionConfig(string name, string enittiesName)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "�����������Ʋ���Ϊ�ա�");
            }
            Name = name;
            if (string.IsNullOrEmpty(enittiesName))
            {
                throw new ArgumentNullException("enittiesName", "��������ʵ�����Ʋ���Ϊ�ա�");
            }
            EntitiesName = enittiesName;
        }
        public string Name { get; }
        public string EntitiesName { get; }
    }
}