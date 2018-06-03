﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Diagnostics;
using Zhoubin.Infrastructure.Common.Config;
using Zhoubin.Infrastructure.Common.Extent;

namespace Zhoubin.Infrastructure.Common.Cache
{
    public class CacheConfig : ConfigEntityBase
    {
        #region Membership Variables        

        /// <summary>
        /// 构造函数
        /// </summary>
        public CacheConfig()
        {
            KeySuffix = string.Empty;
            DefaultExpireTime = 2000;
        }

        #endregion


        /// <summary>
        /// 默认过期时间
        /// 单位毫秒
        /// </summary>
        public ulong DefaultExpireTime { get; set; }

        /// <summary>
        /// Key前缀
        /// </summary>
        public string KeySuffix { get; set; }



        protected override void SetProperty(IConfigurationSection node)
        {
            base.SetProperty(node);
            switch (node.Key)
            {
                case "defaultExpireTime":
                    DefaultExpireTime =
                    Convert.ToUInt64(GetConfigValue(node.Value, "2000"));
                    break;
                case "keySuffix":
                    KeySuffix =
                    GetConfigValue(node.Value, string.Empty);
                    break;
                case "Provider":
                    Provider = (CacheProvider)node.Value.CreateInstance();
                    break;
            }
        }

        public override void Parse(IConfigurationSection node)
        {
            base.Parse(node);
            if(Provider != null)
            {
                Provider.DefaultExpireTime = DefaultExpireTime;
                Provider.KeySuffix = KeySuffix;
            }
        }

        public CacheProvider Provider
        {
            get;private set;
        }

        static string GetConfigValue(string configValue, string defaultValue)
        {
            return (string.IsNullOrEmpty(configValue) ? defaultValue : configValue);
        }
    }

    /// <summary>
    /// CacheProvider基类
    /// 所有实现都从此基类继承
    /// </summary>
    public abstract class CacheProvider :IDisposable
    {
        #region Membership Variables        

        /// <summary>
        /// 构造函数
        /// </summary>
        protected CacheProvider(string providerName)
        {
            ProviderName = providerName;
            KeySuffix = string.Empty;
            DefaultExpireTime = 2000;
        }

        #endregion
        
        public string ProviderName { get; private set; }
        /// <summary>
        /// 默认过期时间
        /// 单位毫秒
        /// </summary>
        public ulong DefaultExpireTime { get; set; }

        /// <summary>
        /// Key前缀
        /// </summary>
        public string KeySuffix { get; set; }

        static string GetConfigValue(string configValue, string defaultValue)
        {
            return (string.IsNullOrEmpty(configValue) ? defaultValue : configValue);
        }

        /// <summary>
        /// 新增缓存对象
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="objValue">Value</param>
        /// <returns>成功返回true,其它返回false</returns>
        public abstract bool Add(string strKey, object objValue);
        /// <summary>
        /// 新增缓存对象
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="objValue">Value</param>
        /// <param name="bDefaultExpire">是否默认过期时间</param>
        /// <returns>成功返回true,其它返回false</returns>
        public bool Add(string strKey, object objValue, bool bDefaultExpire)
        {
            if (!bDefaultExpire)
            {
                return Add(strKey, objValue);
            }

            return Add(strKey, objValue, TimeSpan.FromSeconds(DefaultExpireTime));
        }
        /// <summary>
        /// 新增缓存对象
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="objValue">Value</param>
        /// <param name="lNumofMilliSeconds">过期时间，在当前日期的基础上加上此时间，单位毫秒</param>
        /// <returns>成功返回true,其它返回false</returns>
        public bool Add(string strKey, object objValue, long lNumofMilliSeconds)
        {
            return Add(strKey, objValue,TimeSpan.FromMilliseconds(lNumofMilliSeconds));
        }
        /// <summary>
        /// 新增缓存对象
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="objValue">Value</param>
        /// <param name="timeSpan">过期时间，在当前日期的基础上加上此时间</param>
        /// <returns>成功返回true,其它返回false</returns>
        public abstract bool Add(string strKey, object objValue, TimeSpan timeSpan);

        /// <summary>
        /// 新增缓存对象
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="objValue">Value</param>
        /// <param name="time">绝对过期日期</param>
        /// <returns>成功返回true,其它返回false</returns>
        public bool Add(string strKey, object objValue, DateTime time)
        {
            return Add(strKey, objValue, TimeSpan.FromTicks((time.ToUniversalTime() - DateTime.UtcNow).Ticks));
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <returns>返回缓存对象</returns>
        public abstract object Get(string strKey);
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <returns>成功返回读取到的对象,失败返回默认值</returns>
        public abstract T Get<T>(string strKey);
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="keys">一组Key</param>
        /// <returns>返回一组数据组对象</returns>
        public abstract IDictionary<string, object> Get(params string[] keys);
        /// <summary>
        /// 移除所有缓存
        /// </summary>
        public abstract void RemoveAll();
        /// <summary>
        /// 移除指定键缓存
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <returns>成功返回true,其它返回false</returns>
        public abstract bool Remove(string strKey);
        /// <summary>
        /// 增加缓存时间
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="amount">时间</param>
        /// <returns>返回增加后的时间</returns>
        public abstract ulong Increment(string key, ulong amount);
        /// <summary>
        /// 减少缓存时间
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="amount">时间</param>
        /// <returns>返回减少后的缓存时间</returns>
        public abstract ulong Decrement(string key, ulong amount);


        

        /// <summary>
        /// 释放对象
        /// </summary>
        public abstract void Dispose();

        #region 滑动日期实现
        /// <summary>
        /// 新增默认日期滑动缓存对象
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="objValue">Value</param>
        /// <returns>成功返回true,其它返回false</returns>
        public bool AddForSliding(string strKey, object objValue)
        {
            return AddForSliding(strKey, objValue, TimeSpan.FromMilliseconds(DefaultExpireTime));
        }
        /// <summary>
        /// 新增缓存对象
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="objValue">Value</param>
        /// <param name="lNumofMilliSeconds">过期时间，在当前日期的基础上加上此时间，单位毫秒</param>
        /// <returns>成功返回true,其它返回false</returns>
        public bool AddForSliding(string strKey, object objValue, long lNumofMilliSeconds)
        {
            return AddForSliding(strKey, objValue, TimeSpan.FromMilliseconds(lNumofMilliSeconds));
        }
        /// <summary>
        /// 新增缓存对象
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="objValue">Value</param>
        /// <param name="timeSpan">过期时间，在当前日期的基础上加上此时间</param>
        /// <returns>成功返回true,其它返回false</returns>
        public abstract bool AddForSliding(string strKey, object objValue, TimeSpan timeSpan);
        #endregion
    }


}