//using ABCBrasil.Core.CacheRedis.Lib.Ext;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Cache;
//using Microsoft.Extensions.Options;
//using StackExchange.Redis;
//using StackExchange.Redis.Extensions.Core.Configuration;
//using System.Threading.Tasks;
//using static ABCBrasil.OpenBanking.BackOfficeTed.Core.Common.Shared;

//namespace ABCBrasil.OpenBanking.BackOfficeTed.Infra.Cache
//{
//    public class CoreCipCache : ICipCache
//    {
//        readonly IConnectionMultiplexer _cache;
//        public CoreCipCache(IOptions<RedisConfiguration> options, IConnectionMultiplexer cache) 
//        {
//            _cache = cache;
//        }

//        /// <summary>
//        /// Método responsável por armazenar no cache um boleto consultado na CIP
//        /// </summary>
//        /// <param name="request">Boleto válido CIP</param>
//        /// <param name="codigoDeBarras">Código de barras</param>
//        /// <param name="ttl">Time to leave - Tempo de armazenamento limite em segundos</param>
//        /// <returns></returns>
//        public async Task<bool> Create(object request, string codigoDeBarras, int ttl)
//        {
//            return await _cache.SetAsync($"{Configuration.CACHE_MAIN_KEY}{Configuration.CACHE_CIP_KEY}{codigoDeBarras}", request, ttl);
//        }
//    }
//}
