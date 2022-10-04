using Microsoft.Extensions.Caching.Memory;
using ShishaProject.Common.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ShishaProject.Common.Caching
{
    public class ShishaCache : IShishaCache
    {
        private readonly IMemoryCache memoryCache;

        public ShishaCache(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public bool Contains(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            this.memoryCache.TryGetValue<ConcurrentDictionary<string, T>>(typeof(T), out var collection);

            if (collection != null && collection.ContainsKey(key))
            {
                return collection[key];
            }

            return default(T);
        }

        //public void Set<T>(string key, T value, TimeSpan expires) =>
        //   this.memoryCache.Set<T>(key, value, expires);

        public void Set<T>(string key, T value)
        {

            var itemToGet = this.Get<T>(key);

            if (itemToGet == null)
            {
                //this.memoryCache.Set<>
            }

        }
    }
}
