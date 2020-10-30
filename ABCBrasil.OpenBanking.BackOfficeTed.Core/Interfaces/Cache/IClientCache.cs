using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Cache
{
    public interface IClientCache
    {
        Task<bool> Create(IEnumerable<Client> client, short pageNumber, short rowsPerPage, int ttl);
        Task<Client> Create(Client client, int ttl);
        Task<Client> Find(Guid key);
        Task<IEnumerable<Client>> FindAll(short pageNumber, short rowsPerPage);
        Task<bool> Delete(Guid key);
        Task<bool> Update(Guid key, Client client);
    }
}
