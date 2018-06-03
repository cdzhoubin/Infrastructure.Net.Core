using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Zhoubin.Infrastructure.Net.Core.Common.Config
{
    public class ConfigHelp<T> : ConfigHelp where T : ConfigEntityBase<T>,new()
    {
        /// <summary>
        /// Ĭ������
        /// </summary>
        public T DefaultConfig { get { return _section.DefaultConfig; } }

        /// <summary>
        /// �����������
        /// </summary>
        protected List<T> Section { get { return _section.Enities; } }
        /// <summary>
        /// ����������ֵȡָ��������
        /// </summary>
        /// <param name="name"></param>
        public T this[string name]
        {
            get { return Section.FirstOrDefault(p => p.Name == name); }
        }

        /// <summary>
        /// ����������ֵȡָ��������
        /// </summary>
        /// <param name="index"></param>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Section.Count - 1)
                {
                    return null;
                }

                return Section[index];
            }
        }
        /// <summary>
        /// ���������
        /// </summary>
        public int Count
        {
            get { return Section.Count; }
        }
        private SectionConfig _sectionConfig;
        private ConfigSectionEntity<T> _section;
        public ConfigHelp(SectionConfig sectionConfig)
        {
            _sectionConfig = sectionConfig;
            _section = new ConfigSectionEntity<T>();
            var section = ConfigurttionProvider.GetSection(_sectionConfig.Name);
            if (section == null)
            {
                throw new InfrastructureConfigException("�����ļ���û���ҵ���������"+ _sectionConfig.Name);
            }
            
            _section.DefaultConfigName = section.GetValue<string>("Default");
            var configurationSection = section.GetSection(_sectionConfig.EntitiesName);
            if (configurationSection == null)
            {
                throw new InfrastructureConfigException("����������û���ҵ����ýڵ㣺" + _sectionConfig.EntitiesName);
            }
            foreach (var entitySection in configurationSection.GetChildren())
            {
                _section.Enities.Add(new T().Parse(entitySection));
            }
            
        }
    }

    public class ConfigHelp
    {
        protected IConfigurationRoot ConfigurttionProvider
        {
            get
            {
                if (_configurationProvider == null)
                {
                    lock (typeof(ConfigHelp))
                    {
                        if (_configurationProvider == null)
                        {
                            Load();
                        }
                    }
                }
                return _configurationProvider;
            }
        }
        private static IConfigurationRoot _configurationProvider;

        protected virtual string ConfigFile
        {
            get { return Path.Combine(Directory.GetCurrentDirectory(), "config.json"); }
        }
        private void Load()
        {
            var builder = JsonConfigurationExtensions.AddJsonFile(new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()), (string) ConfigFile);
            _configurationProvider = builder.Build();
        }
        
    }
}