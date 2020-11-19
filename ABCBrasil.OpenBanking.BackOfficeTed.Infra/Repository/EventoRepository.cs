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
    public class EventoRepository : DapperRepository, IEventoRepository
    {
        public EventoRepository(IConnectDataBase connectDataBase) : base(connectDataBase)
        {
        }

        public async Task<bool> AtualizaEnvio(int Cd_Evento_Api)
        {
            var param = new { CD_EVENTO_API = Cd_Evento_Api };
            return await QueryFirstOrDefaultAsync<bool>(SqlProc.InsertTeds_Proc, param, System.Data.CommandType.StoredProcedure);
        }

        public async Task<List<TedInfo>> BuscaTeds(BuscaTedRequest request)
        {
            var param = new { DT_INI = request.DTINI, DT_FIM = request.DTFIM, QTD = request.QTD };
            return await QueryFirstOrDefaultAsync<List<TedInfo>>(SqlProc.BuscaTedsReprocessar_Proc, param.QTD, System.Data.CommandType.StoredProcedure);
        }
        public async Task<bool> InsereTeds(TedInfo ted)
        { 
            return    await QueryFirstOrDefaultAsync<bool>(SqlProc.InsertTeds_Proc, ted, System.Data.CommandType.StoredProcedure);
        }
    }
}
