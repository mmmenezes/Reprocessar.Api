using ABCBrasil.Core.Data;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
using ABCBrasil.SegurancaApi.DSL.Libs.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Infra.Repository
{
    public class CipRepository : DapperRepository, ICipRepository
    {
        readonly IAntiCSRFService _antiCSRFService;
        public CipRepository(IAntiCSRFService antiCSRFService, IConnectDataBase connectDataBase) : base(connectDataBase)
        {
            _antiCSRFService = antiCSRFService;
        }

        public async Task<bool> ValidarCrcUsuario(int CodigoCrc)
        {
            var param = new { CD_CRC = CodigoCrc, DC_LOGIN = _antiCSRFService.Login };
            return await base.QueryFirstOrDefaultAsync<bool>(SqlProc.ValidaCliente_Proc, param, CommandType.StoredProcedure);
        }
    }
}
