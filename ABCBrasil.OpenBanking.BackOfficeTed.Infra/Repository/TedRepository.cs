using ABCBrasil.Core.Data;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Infra.Repository
{
    public class TedRepository : DapperRepository, ITedRepository
    {
        public TedRepository(IConnectDataBase connectDataBase) : base(connectDataBase)
        {
        }

        public async Task<List<TedInfo>> BuscaEInsereTeds(BuscaTedRequest request)
        {
            var param = new { DT_INI = request.DTINI, DT_FIM = request.DTFIM, QTD = request.QTD };
            await QueryFirstOrDefaultAsync<object>(SqlProc.InsertTeds_Proc, param, System.Data.CommandType.StoredProcedure);
            return await QueryFirstOrDefaultAsync<List<TedInfo>>(SqlProc.BuscaTedsReprocessar_Proc, param.QTD, System.Data.CommandType.StoredProcedure);
        }
    }
}
