using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace ProxyService
{
    internal class ProxyCache<T>
    {
        private readonly ObjectCache _cache = MemoryCache.Default;
        private readonly Func<string, Task<T>> _defaultHandler;
        
        public ProxyCache(Func<string, Task<T>> defaultHandler)
        {
            _defaultHandler = defaultHandler;
        }

        public async Task<T> Get(string key)
        {
            return await Get(key, 60);
        }

        public async Task<T> Get(string key, double seconds)
        {
            return await Get(key, DateTimeOffset.Now.AddSeconds(seconds));
        }

        public async Task<T> Get(string key, DateTimeOffset offset)
        {
            if (_cache.Get(key) is T x)
            {
                return x;
            }

            var value = (await _defaultHandler(key))!;
            _cache.Add(key, value, offset);
            return value;
        }
    }
}