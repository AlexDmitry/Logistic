using ITSK.SSS.Base.Enums;
using System;
using System.Collections;
using System.Linq;

namespace ITSK.SSS.Base.Caching
{
    /// <summary>
    /// Cache Service.
    /// Пример использования
    /// List&lt;Employee&gt; empList = GetFromCache("myemployeename",delegate(){
    /// return EmployeeManager.GetEmployees();
    /// });
    /// </summary>
    public class CacheService
    {
        protected static readonly object GetFromCacheLocker = new object();
        protected readonly System.Web.Caching.Cache _cache;

        public CacheService(System.Web.Caching.Cache cache)
        {
            _cache = cache;
        }

        private static TimeSpan GetExpirationTimeSpan(HttpCache expiraton)
        {
            switch (expiraton)
            {
                case HttpCache.CommonData: return new TimeSpan(0, 5, 0);
                case HttpCache.Statistic: return new TimeSpan(0, 30, 0);
                case HttpCache.Config: return new TimeSpan(0, 30, 0);
                case HttpCache.Domain: return new TimeSpan(1, 0, 0);
                default:
                    return new TimeSpan(0, 5, 0);
            }
        }

        public T GetFromCache<T>(string key, Func<T> loader, HttpCache expiraton = HttpCache.Default, bool forceRead = false)
            where T : class
        {
            T entityData;
            if (!forceRead && _cache != null && _cache[key] != null)
            {
                var obj = _cache[key];
                var data = obj as T;
                if (data != null)
                {
                    entityData = data;
                }
                else
                {
                    entityData = loader();
                    SetToCache(key, entityData, expiraton);
                }
            }
            else
            {
                entityData = loader();
                SetToCache(key, entityData, expiraton);
            }
            return entityData;
        }

        public void SetToCache(string key, object obj)
        {
            SetToCache(key, obj, HttpCache.Default);
        }

        public void SetToCache(string key, object obj, HttpCache expiraton)
        {
            if (_cache == null || key == null)
            {
                return;
            }

            lock (GetFromCacheLocker)
            {
                if (obj != null)
                {
                    _cache.Insert(
                        key, obj, null,
                        DateTime.Now.Add(GetExpirationTimeSpan(expiraton)),
                        System.Web.Caching.Cache.NoSlidingExpiration
                        );
                }
                else
                {
                    _cache.Remove(key);
                }
            }
        }

        public void RemoveFromCache(string key)
        {
            if (_cache == null)
            {
                return;
            }

            if (key != null)
            {
                _cache.Remove(key);
            }
        }
    }
}
