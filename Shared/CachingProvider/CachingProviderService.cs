using Microsoft.Extensions.Caching.Memory;
using System.Dynamic;

namespace Shared.CachingProvider
{
    public interface ICachingProviderService
    {
        object GetItem(string key);
        object GetItem<T>(string key);
        void SetItem(string key, string value);
        void DeleteItem(string key);
    }

    public class MemoryCacheProvider : ICachingProviderService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public object GetItem(string key)
        {
            _memoryCache.TryGetValue(key, out object value);
            return value;
        }

        public object GetItem<T>(string key)
        {
            _memoryCache.TryGetValue<T>(key, out T value);
            return value;
        }

        //TODO: Settings from appSettings
        public void SetItem(string key, string value)
        {
            _memoryCache.Set(key, value);
        }

        public void DeleteItem(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}