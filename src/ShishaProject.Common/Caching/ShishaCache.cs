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

            if (string.IsNullOrEmpty(key) && collection != null && collection.ContainsKey(key))
            {
                return collection[key];
            }

            return default(T);
        }

        public void Set<T>(string key, T value)
        {
            var collection = this.memoryCache.GetOrCreate<ConcurrentDictionary<string, T>>(typeof(T), x =>
            {
                x.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2);
                return new ConcurrentDictionary<string, T>();
            });

            collection.AddOrUpdate(key, value, (oldkey, oldvalue) => value);
        }
    }
}
