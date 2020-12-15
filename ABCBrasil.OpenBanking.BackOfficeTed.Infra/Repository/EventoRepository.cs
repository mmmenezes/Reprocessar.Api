﻿using ABCBrasil.Core.Data;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Infra.Repository
{
    public class EventoRepository : DapperRepository, IEventoRepository
    {
        public EventoRepository(IConnectDataBase connectDataBase) : base(connectDataBase)
        {
        }

        public async Task<IEnumerable<string>> AtualizaEnvio(int Cd_Evento_Api)
        {
            var param = new { CD_EVENTO_API = Cd_Evento_Api };
            return await QueryFirstOrDefaultAsync<IEnumerable<string>>(SqlProc.UpdateTrans_Proc, param, System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<TedInfo>> BuscaTeds(BuscaTedRequest request)
        {
            if(request.CDCliente == null)
                request.CDCliente = 0;
            var param  = new { DT_INI = request.DTINI, DT_FIM = request.DTFIM, CDCRC = request.CDCliente };
            return Query<TedInfo>(SqlProc.BuscaTedsReprocessar_Proc, param);
           
           
        }

        public async Task<IEnumerable<string>> BuscaUser(string Protocolo)
        {
            var param = new { Protocolo = Protocolo };
            return Query<string>(SqlProc.BuscaUserReprocessarTed, param);
        }

        public async Task<IEnumerable<bool>> InsereTeds(TedInfo ted)
        {
            var param = new { Cd_Evento_Api = ted.Cd_Evento_Api, Gw_Evento_Api = ted.Gw_Evento_Api, Dc_Payload_Request = ted.Dc_Payload_Request };
            return Query<bool>(SqlProc.InsertTeds_Proc, param);
        }
    }
}
