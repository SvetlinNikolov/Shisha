using ShishaProject.Common.Enums;
using System;

namespace ShishaProject.Common.Caching
{
    public interface IShishaCache
    {
        //public T Get<T>(string key/*, CacheItemType cacheItemType*/);

        public T Get<T>(string key);

        //public void Set<T>(string key, T value, TimeSpan expires/*, CacheItemType cacheItemType*/);

        public void Set<T>(string key, T value, TimeSpan expires);

        //public void Set<T>(string key, T value, CacheItemType cacheItemType);

        public void Set<T>(string key, T value);
    }
}
