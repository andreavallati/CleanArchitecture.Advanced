using Microsoft.Extensions.Caching.Memory;
using CleanArchitecture.Advanced.Api.Application.Interfaces.Caching;

namespace CleanArchitecture.Advanced.Api.Infrastructure.Caching
{
    public class CustomMemoryCache : ICustomMemoryCache
    {
        private IMemoryCache Cache { get; set; }

        private TimeSpan CacheExpirationTime => TimeSpan.FromMinutes(5);

        public CustomMemoryCache(IMemoryCache cache)
        {
            Cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public IEnumerable<T> GetSetItemsCache<T>(Func<IEnumerable<T>> action, string key) where T : class
        {
            return GetSetItemsCache(action, key, CacheExpirationTime);
        }

        public IEnumerable<T> GetSetItemsCache<T>(Func<IEnumerable<T>> action, string key, TimeSpan expiration) where T : class
        {
            var keyToSet = key ?? typeof(T).UnderlyingSystemType.ToString();

            if (Cache.TryGetValue(key, out IEnumerable<T> cachedObjects))
            {
                // Key exists in cache
                return cachedObjects;
            }

            // Key not exists in cache
            cachedObjects = action();
            Cache.Set(keyToSet, cachedObjects, new MemoryCacheEntryOptions { SlidingExpiration = expiration });
            return cachedObjects;
        }

        public async Task<IEnumerable<T>> GetSetItemsCacheAsync<T>(Func<Task<IEnumerable<T>>> action, string key)
        {
            return await GetSetItemsCacheAsync(action, key, CacheExpirationTime);
        }

        public async Task<IEnumerable<T>> GetSetItemsCacheAsync<T>(Func<Task<IEnumerable<T>>> action, string key, TimeSpan expiration)
        {
            var keyToSet = key ?? typeof(T).UnderlyingSystemType.ToString();

            if (Cache.TryGetValue(key, out IEnumerable<T> cachedObjects))
            {
                // Key exists in cache
                return cachedObjects;
            }

            // Key not exists in cache
            cachedObjects = await action();
            Cache.Set(keyToSet, cachedObjects, new MemoryCacheEntryOptions { SlidingExpiration = expiration });
            return cachedObjects;
        }

        public IEnumerable<T> RefreshItemsCache<T>(Func<IEnumerable<T>> action, string key) where T : class
        {
            return RefreshItemsCache(action, key, CacheExpirationTime);
        }

        public IEnumerable<T> RefreshItemsCache<T>(Func<IEnumerable<T>> action, string key, TimeSpan expiration) where T : class
        {
            Cache.Remove(key);
            return GetSetItemsCache(action, key, expiration);
        }

        public T GetSetItemCache<T>(Func<T> action, string key)
        {
            return GetSetItemCache(action, key, CacheExpirationTime);
        }

        public T GetSetItemCache<T>(Func<T> action, string key, TimeSpan expiration)
        {
            if (Cache.TryGetValue(key, out T cachedObject))
            {
                // Key exists in cache
                return cachedObject;
            }

            // Key not exists in cache
            cachedObject = action();
            Cache.Set(key, cachedObject, new MemoryCacheEntryOptions { SlidingExpiration = expiration });
            return cachedObject;
        }

        public async Task<T> GetSetItemCacheAsync<T>(Func<Task<T>> action, string key)
        {
            return await GetSetItemCacheAsync(action, key, CacheExpirationTime);
        }

        public async Task<T> GetSetItemCacheAsync<T>(Func<Task<T>> action, string key, TimeSpan expiration)
        {
            if (Cache.TryGetValue(key, out T cachedObject))
            {
                // Key exists in cache
                return cachedObject;
            }

            // Key not exists in cache
            cachedObject = await action();
            Cache.Set(key, cachedObject, new MemoryCacheEntryOptions { SlidingExpiration = expiration });
            return cachedObject;
        }

        public T RefreshItemCache<T>(Func<T> action, string key) where T : class
        {
            return RefreshItemCache(action, key, CacheExpirationTime);
        }

        public T RefreshItemCache<T>(Func<T> action, string key, TimeSpan expiration) where T : class
        {
            Cache.Remove(key);
            return GetSetItemCache(action, key, expiration);
        }

        public void ClearCache()
        {
            Cache = new MemoryCache(new MemoryCacheOptions());
        }

        public void RemoveCacheBykey(string key)
        {
            Cache.Remove(key);
        }
    }
}
