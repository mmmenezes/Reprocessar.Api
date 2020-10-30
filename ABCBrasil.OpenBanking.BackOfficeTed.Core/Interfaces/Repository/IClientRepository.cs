using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository
{
    public interface IClientRepository
    {
        Task<bool> InsertAsync(Client model);
        Task<Client> FindAsync(Guid key);
        Task<IEnumerable<Client>> SearchAsync(short pageNumber, short rowsPerPage);
        Task<bool> DeleteAsync(Guid key);
        Task<bool> UpdateAsync(Client model);
    }
}
