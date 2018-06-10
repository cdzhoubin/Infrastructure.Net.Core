

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Couchbase;
using Zhoubin.Infrastructure.Common.Extent;

namespace Zhoubin.Infrastructure.Common.Cache.MemcachedCache
{
    /// <summary>
    /// Couchbase滑动缓存实现
    /// </summary>
    public class MemcachedCacheSlidingProvider : SlidingCacheWaperProvider
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MemcachedCacheSlidingProvider()
            : base("Memcached Cache Sliding Provider")
        {

        }



        #region ProviderBase Methods
        private CacheConfig _config;
        /// 初始化
        /// </summary>
        /// <param name="config">配置数据</param>
        /// <exception cref="ArgumentNullException">当参数config为null时抛出此异常</exception>
        public override void Initialize(CacheConfig config)
        {
            _config = config;
            var types = config.ExtentProperty["SerializeTypes"];
            if (types != null)
            {
                var typeArry = types.Split(';');
                _serializeTypes = new Type[typeArry.Length];
                for (int i = 0; i < typeArry.Length; i++)
                {
                    _serializeTypes[i] = typeArry[i].CreateInstance().GetType();
                }
            }
        }

        #endregion

        #region Cache Provider


        /// <summary>
        /// 新增缓存对象
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="objValue">Value</param>
        /// <returns>成功返回true,其它返回false</returns>
        public override bool Add(string strKey, object objValue)
        {
            using (Cluster _cluster = new Cluster())
            using (var bucket = _cluster.OpenBucket())

                return bucket.Replace(KeySuffix + strKey, CreateValueWraper(objValue).SerializeObject(_serializeTypes)).Success;
        }

        /// <inheritdoc />
        public override bool Add(string strKey, object objValue, TimeSpan timeSpan)
        {

            using (Cluster _cluster = new Cluster())
            using (var bucket = _cluster.OpenBucket())
                return bucket.Replace(KeySuffix + strKey,
                                 CreateValueWraper(objValue).SerializeObject(_serializeTypes), timeSpan).Success;
        }

        /// <summary>
        /// 使用包装器实现滑动
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="result">解析结果</param>
        /// <param name="strKey">键值</param>
        /// <returns></returns>
        private T GetValue<T>(SlidingCacheWraper result, string strKey)
        {

            using (Cluster _cluster = new Cluster())
            using (var bucket = _cluster.OpenBucket())
                return GetValue<T>(result, strKey, (p, v, t) => bucket.Replace(p, v.SerializeObject(_serializeTypes), t));
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <returns>返回缓存对象</returns>
        public override object Get(string strKey)
        {

            using (Cluster _cluster = new Cluster())
            using (var bucket = _cluster.OpenBucket())
            {
                var value = bucket.Get<string>(KeySuffix + strKey);
                if (!value.Success)
                {
                    return null;
                }
                return GetValue<object>(value.Value.DeserializeObject<SlidingCacheWraper>(_serializeTypes), KeySuffix + strKey);
            }
        }


        /// <inheritdoc />
        public override IDictionary<string, object> Get(params string[] keys)
        {
            IList<string> keysList = keys.Select(str => KeySuffix + str).ToList();
            using (Cluster _cluster = new Cluster())
            using (var bucket = _cluster.OpenBucket())
            {
                IDictionary<string, object> retVal = new Dictionary<string, object>();
                foreach (var key in keysList)
                {
                    var task = bucket.GetAsync<object>(key);
                    if (task.Result.Success)
                    {
                        retVal.Add(key.Remove(0, KeySuffix.Length), task.Result.Value);
                    }
                }

                return retVal;
            }
        }

        /// <summary>
        /// 移除所有缓存
        /// </summary>
        public override void RemoveAll()
        {

            using (Cluster _cluster = new Cluster())
            using (var bucket = _cluster.OpenBucket())
            {
                var user = _config.ExtentProperty["User"];
                var manager = string.IsNullOrEmpty(user) ? bucket.CreateManager() : bucket.CreateManager(user, _config.ExtentProperty["Password"]);
                var result = manager.Flush();
            }
        }

        /// <inheritdoc />
        public override bool Remove(string strKey)
        {

            using (Cluster _cluster = new Cluster())
            using (var bucket = _cluster.OpenBucket())
                return bucket.Remove(KeySuffix + strKey).Success;
        }


        /// <inheritdoc />
        public override T Get<T>(string strKey)
        {
            using (Cluster _cluster = new Cluster())
            using (var bucket = _cluster.OpenBucket())
            {
                var value = bucket.Get<string>(KeySuffix + strKey);
                if (!value.Success)
                {
                    return default(T);
                }
                return GetValue<T>(value.Value.DeserializeObject<SlidingCacheWraper>(_serializeTypes), KeySuffix + strKey);
            }
        }


        /// <inheritdoc />
        public override ulong Increment(string key, ulong amount)
        {
            using (Cluster _cluster = new Cluster())
            using (var bucket = _cluster.OpenBucket())
            {
                var result = bucket.Increment(KeySuffix + key, DefaultExpireTime, amount);
                if (result.Success)
                {
                    return result.Value;
                }

                return 0;
            }
        }


        /// <inheritdoc />
        public override ulong Decrement(string key, ulong amount)
        {
            using (Cluster _cluster = new Cluster())
            using (var bucket = _cluster.OpenBucket())
            {
                var result = bucket.Decrement(KeySuffix + key, DefaultExpireTime, amount);
                if (result.Success)
                {
                    return result.Value;
                }

                return 0;
            }
        }

        #endregion



        /// <inheritdoc />
        public override void Dispose()
        {
        }

        /// <inheritdoc />
        public override bool AddForSliding(string strKey, object objValue, TimeSpan timeSpan)
        {
            using (Cluster _cluster = new Cluster())
            using (var bucket = _cluster.OpenBucket())
                return bucket.Replace(KeySuffix + strKey,
                                 CreateValueWraper(objValue, timeSpan).SerializeObject(_serializeTypes), timeSpan).Success;
        }

        private Type[] _serializeTypes;
    }
}
