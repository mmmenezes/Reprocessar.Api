using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository
{
    public interface IIBRepository
    {
       Task<IEnumerable<string>> ProcessaTed(TransferenciaInclui transferenciaInclui);
    }
}
