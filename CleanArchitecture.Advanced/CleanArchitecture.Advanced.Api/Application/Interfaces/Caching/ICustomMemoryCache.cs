namespace CleanArchitecture.Advanced.Api.Application.Interfaces.Caching
{
    public interface ICustomMemoryCache
    {
        IEnumerable<T> GetSetItemsCache<T>(Func<IEnumerable<T>> action, string key) where T : class;
        IEnumerable<T> GetSetItemsCache<T>(Func<IEnumerable<T>> action, string key, TimeSpan expiration) where T : class;
        Task<IEnumerable<T>> GetSetItemsCacheAsync<T>(Func<Task<IEnumerable<T>>> action, string key);
        Task<IEnumerable<T>> GetSetItemsCacheAsync<T>(Func<Task<IEnumerable<T>>> action, string key, TimeSpan expiration);
        IEnumerable<T> RefreshItemsCache<T>(Func<IEnumerable<T>> action, string key) where T : class;
        IEnumerable<T> RefreshItemsCache<T>(Func<IEnumerable<T>> action, string key, TimeSpan expiration) where T : class;
        T GetSetItemCache<T>(Func<T> action, string key);
        T GetSetItemCache<T>(Func<T> action, string key, TimeSpan expiration);
        Task<T> GetSetItemCacheAsync<T>(Func<Task<T>> action, string key);
        Task<T> GetSetItemCacheAsync<T>(Func<Task<T>> action, string key, TimeSpan expiration);
        T RefreshItemCache<T>(Func<T> action, string key) where T : class;
        T RefreshItemCache<T>(Func<T> action, string key, TimeSpan expiration) where T : class;
        void ClearCache();
        void RemoveCacheBykey(string key);
    }
}
