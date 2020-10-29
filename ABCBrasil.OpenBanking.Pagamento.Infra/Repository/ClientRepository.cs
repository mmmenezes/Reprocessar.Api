using ABCBrasil.Core.Data;
using ABCBrasil.OpenBanking.Pagamento.Core.Common;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Cache;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.Pagamento.Core.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Infra.Repository
{
    public class ClientRepository : DapperRepository, IClientRepository
    {
        private readonly IClientCache _clientCache;
        readonly IOptionsMonitor<AbcBrasilApiSettings> _apiSettings;

        public ClientRepository(IClientCache clientCache, IOptionsMonitor<AbcBrasilApiSettings> apiSettings, IConnectDataBase connectDataBase) : base(connectDataBase)
        {
            _clientCache = clientCache;
            _apiSettings = apiSettings;
        }

        public async Task<bool> DeleteAsync(Guid key)
        {
            await base.ExecuteAsync(SqlProc.DeleteClient_Proc, new { KEY_CLIENT = key }, CommandType.StoredProcedure);
            return true;
        }
        public async Task<Client> FindAsync(Guid key)
        {
            return await base.QueryFirstOrDefaultAsync<Client>(
                  SqlProc.FindClient_Proc,
                  new { KEY_CLIENT = key }, CommandType.StoredProcedure);
        }
        public async Task<bool> InsertAsync(Client model)
        {
            var param = new
            {
                KEY_CLIENT = model.Key,
                NM_CLIENT = model.Name,
                FLAG_GENRE = (int)model.Genre,
                NR_DOCUMENT = model.Document,
                TP_DOCUMENT = (int)model.TypeDocument,
                FLAG_MARITAL_STATUS = (int)model.MaritalStatus,
                NR_CONTACT = model.Contact
            };
            await base.ExecuteAsync(SqlProc.InsertClient_Proc, param, CommandType.StoredProcedure);
            return true;
        }
        public async Task<IEnumerable<Client>> SearchAsync(short pageNumber, short rowsPerPage)
        {
            if (_apiSettings.CurrentValue.CacheActivated)
            {
                var resultCache = await _clientCache.FindAll(pageNumber, rowsPerPage);
                if (resultCache != null && resultCache?.Count() > 0)
                {
                    return resultCache;
                }
            }

            var result = await base.QueryAsync<Client>(
                        SqlProc.FindClientPagination_Proc,
                        new { PageNumber = pageNumber, RowsPerPage = rowsPerPage }, CommandType.StoredProcedure);

            if (result != null && result.Any() && _apiSettings.CurrentValue.CacheActivated)
            {
                await _clientCache.Create(result, pageNumber, rowsPerPage, _apiSettings.CurrentValue.CacheTtl);
            }

            return result;
        }
        public async Task<bool> UpdateAsync(Client model)
        {
            var param = new
            {
                KEY_CLIENT = model.Key,
                NM_CLIENT = model.Name,
                FLAG_GENRE = (int)model.Genre,
                NR_DOCUMENT = model.Document,
                TP_DOCUMENT = (int)model.TypeDocument,
                FLAG_MARITAL_STATUS = (int)model.MaritalStatus,
                NR_CONTACT = model.Contact
            };
            await base.ExecuteAsync(SqlProc.UpdateClient_Proc, param, CommandType.StoredProcedure);
            return true;
        }
    }
}
