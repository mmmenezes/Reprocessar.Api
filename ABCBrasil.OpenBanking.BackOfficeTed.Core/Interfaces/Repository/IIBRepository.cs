using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository
{
    public interface IIBRepository
    {
       Task<IEnumerable<string>> ProcessaTed(TransferenciaInclui transferenciaInclui);
    }
}
