using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Infra.Cache
{
    public class CacheBase : IDisposable
    {
        static string _cacheServer;
        static string _cacheServerAndPort;
        protected readonly int DataBaseCorporate;

        public CacheBase(IOptions<RedisConfiguration> options)
        {
            DataBaseCorporate = options?.Value?.Database ?? 1;
            SetCacheServer(options?.Value);
        }

        static void SetCacheServer(RedisConfiguration config)
        {
            _cacheServer = config?.ConfigurationOptions?.ToString();

            var host = config?.Hosts?.FirstOrDefault();
            _cacheServerAndPort = $"{host?.Host}:{host?.Port}";
        }

        readonly Lazy<IConnectionMultiplexer> _lazyConnection =
             new Lazy<IConnectionMultiplexer>
             (
                 () => ConnectionMultiplexer.Connect(_cacheServer)
             );

        IServer Server => Connection.GetServer(_cacheServerAndPort);
        IConnectionMultiplexer Connection => _lazyConnection.Value;
        IDatabase Database => Connection.GetDatabase(DataBaseCorporate);
        public async Task<T> GetAsync<T>(string key)
        {
            if (Connection.IsConnected)
            {
                var content = await Database.StringGetAsync(key);
                if (!content.IsNull)
                    return JsonConvert.DeserializeObject<T>(content);
            }
            return default;
        }
        public async Task<RedisValue[]> GetAllAsync(string key)
        {
            var result = default(RedisValue[]);
            if (Connection.IsConnected)
            {
                var keys = Server.Keys(database: DataBaseCorporate, pattern: key);
                if (keys?.Any() ?? false)
                {
                    result = await Database.StringGetAsync(keys.ToArray());
                }
            }
            return result;
        }
        public async Task<bool> SetAsync<T>(string key, T value, int ttl)
        {
            var expiry = TimeSpan.FromSeconds(ttl);

            var result = false;
            if (Connection.IsConnected)
            {
                var content = JsonConvert.SerializeObject(value, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                result = await Database.StringSetAsync(key, content, expiry, When.Always);
            }
            return result;
        }
        public async Task<bool> ExistsAsync(string key)
        {
            var result = false;
            if (Connection.IsConnected)
            {
                result = await Database.KeyExistsAsync(key);
            }
            return result;
        }
        public async Task<bool> DeleteAsync(string key)
        {
            var result = false;
            if (Connection.IsConnected)
            {
                result = await Database.KeyDeleteAsync(key);
            }
            return result;
        }

        #region IDisposable
        bool disposed = false;
        readonly System.Runtime.InteropServices.SafeHandle handle = new Microsoft.Win32.SafeHandles.SafeFileHandle(IntPtr.Zero, true);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                if (Connection.IsConnected)
                {
                    Connection.Close(true);
                }
                handle.Dispose();
            }
            disposed = true;
        }
        #endregion
    }
}
