using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zhoubin.Infrastructure.Common.Config
{
    /// <summary>
    /// 配置读取类基类
    /// </summary>
    /// <typeparam name="T">配置对象类型</typeparam>
    public class ConfigHelper<T>: ConfigHelper where T : ConfigEntityBase,new()
    {
        /// <summary>
        /// 设置是否自动生成 配置加密key
        /// </summary>
        public static bool EnableAutoGenKey
        {
            get
            {
                return ConfigEntityBase.EnableAutoGenKey;
            }
            set
            {
                ConfigEntityBase.EnableAutoGenKey = value;
            }
        }
        private readonly ConfigSectionEntity<T> _section;
        /// <summary>
        /// 默认配置
        /// </summary>
        public T DefaultConfig { get { return _section.DefaultConfig; } }

        /// <summary>
        /// 解析后的配置
        /// </summary>
        protected List<T> Section { get { return _section.Enities; } }

        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sectionName">配置区名称</param>
        /// <param name="configFile">配置文件路径，当传入null或空时，如果在当前目录或bin目录下存在Common.Config文件，就使用此文件，如果不存在表示从默认配置文件读取</param>
        public ConfigHelper(string sectionName)
        {

            var section = configuration.GetSection(sectionName);
            ConfigSectionEntity<T> configSectionEntity = new ConfigSectionEntity<T>();
            List<T> list = new List<T>();
            foreach(var item in section.GetChildren())
            {
                T entity = new T();
                entity.Parse(item);
                list.Add(entity);
                
            }
            configSectionEntity.Enities = list;
            configSectionEntity.DefaultConfig = list.FirstOrDefault(p => p.Default);
            if(configSectionEntity.DefaultConfig != null)
            {
                configSectionEntity.DefaultConfigName = configSectionEntity.DefaultConfig.Name;
            }
            _section = configSectionEntity;


        }

        

        /// <summary>
        /// 根据索引键值取指定的配置
        /// </summary>
        /// <param name="name"></param>
        public T this[string name]
        {
            get { return Section.FirstOrDefault(p => p.Name == name); }
        }

        /// <summary>
        /// 根据索引键值取指定的配置
        /// </summary>
        /// <param name="index"></param>
        public T this[int index]
        {
            get
            {
                if (index <0 || index >Section.Count-1)
                {
                    return null;
                }

                return Section[index];
            }
        }
        /// <summary>
        /// 配置项个数
        /// </summary>
        public int Count
        {
            get { return Section.Count; }
        }
    }


    public class ConfigHelper
    {
        public static string GetAppSettings(string key) {
            var appseetings = configuration.GetSection("appSetting");
            if(appseetings == null)
            {
                return null;
            }
            var section = appseetings.GetSection(key);
            if (section  == null)
            {
                return null;
            }
            return section.Value;
        }

        protected static IConfiguration configuration;
        static ConfigHelper()
        {
            configuration = CreateConfiguration();
        }
        private string GetConfigFile(string file)
        {
            return string.Format("{0}{1}{2}", AppContext.BaseDirectory.TrimEnd(Path.PathSeparator), Path.PathSeparator, file);
        }

        private static IConfiguration CreateConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings_custom.json", true, true)
                .Build();

        }
    }
}
