//using ABCBrasil.Core.CacheRedis.Lib.Ext;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Cache;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
//using Microsoft.Extensions.Options;
//using StackExchange.Redis;
//using StackExchange.Redis.Extensions.Core.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace ABCBrasil.OpenBanking.BackOfficeTed.Infra.Cache
//{
//    public class ClientCache : IClientCache
//    {
//        readonly IConnectionMultiplexer _cache;
//        const string CACHE_KEY = "urn:clients:client";
//        public ClientCache(IOptions<RedisConfiguration> options, IConnectionMultiplexer cache)
//        {
//            _cache = cache;
//        }

//        public async Task<bool> Create(IEnumerable<Client> client, short pageNumber, short rowsPerPage, int ttl)
//        {
//            return await _cache.SetAsync($"{CACHE_KEY}-{pageNumber}-{rowsPerPage}", client, ttl);
//        }
//        public async Task<Client> Create(Client client, int ttl)
//        {            
//            await _cache.SetAsync($"{CACHE_KEY}:{client.Key}", client, ttl);

//            return await Task.FromResult(client);
//        }
//        public async Task<Client> Find(Guid key)
//        {
//            var result = await _cache.GetAsync<Client>($"{CACHE_KEY}:{key}");
//            return await Task.FromResult(result);
//        }
//        public async Task<IEnumerable<Client>> FindAll(short pageNumber, short rowsPerPage)
//        {
//            return await _cache.GetAsync<IEnumerable<Client>>($"{CACHE_KEY}-{pageNumber}-{rowsPerPage}");
//        }
//        public async Task<bool> Delete(Guid key)
//        {
//            //TODO:Verificar metodo de exclusão.
//            return await Task.FromResult(false);
//        }
//        public async Task<bool> Update(Guid key, Client client)
//        {
//            return await _cache.SetAsync($"{CACHE_KEY}:{key}", client, 5);
//        }
//    }
//}
