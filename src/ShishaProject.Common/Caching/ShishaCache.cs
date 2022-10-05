namespace ShishaProject.Common.Caching
{
    using System;
    using System.Collections.Concurrent;

    using Microsoft.Extensions.Caching.Memory;

    public class ShishaCache : IShishaCache
    {
        private readonly IMemoryCache memoryCache;

        public ShishaCache(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public T Get<T>(string key)
        {
            this.memoryCache.TryGetValue<ConcurrentDictionary<string, T>>(typeof(T), out var collection);

            if (!string.IsNullOrEmpty(key) && collection != null && collection.ContainsKey(key))
            {
                return collection[key];
            }

            return default(T);
        }

        public bool TryGet<T>(string key, out T value)
        {
            value = this.Get<T>(key);

            return !value.Equals(default(T));
        }

        public void SetOrUpdate<T>(string key, T value)
        {
            var collection = this.memoryCache.GetOrCreate<ConcurrentDictionary<string, T>>(typeof(T), cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3);
                return new ConcurrentDictionary<string, T>();
            });

            collection.AddOrUpdate(key, value, (oldkey, oldvalue) => value);
        }
    }
}
