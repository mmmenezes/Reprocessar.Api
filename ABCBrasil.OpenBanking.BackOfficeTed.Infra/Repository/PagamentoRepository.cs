//using ABCBrasil.Core.Data;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Pagamento;
//using ABCBrasil.SegurancaApi.DSL.Libs.Interfaces;
//using System.Data;
//using System.Threading.Tasks;

//namespace ABCBrasil.OpenBanking.BackOfficeTed.Infra.Repository
//{
//    public class PagamentoRepository : DapperRepository, IPagamentoRepository
//    {
//        readonly IAntiCSRFService _antiCSRFService;
//        public PagamentoRepository(IAntiCSRFService antiCSRFService, IConnectDataBase connectDataBase) : base(connectDataBase)
//        {
//            _antiCSRFService = antiCSRFService;
//        }

//        public async Task<bool> ContaPertenceCliente(Core.Models.Pagamento pagamento)
//        {
//            var param = new { CD_CRC = pagamento.CodigoCliente, NR_CONTA = pagamento.Conta };
//            var retorno = await QueryFirstOrDefaultAsync<ContaClienteResponse>(SqlProc.BuscaContasCliente_Proc, param, CommandType.StoredProcedure);

//            if (retorno is null)
//                return false;
//            else
//                return true;
//        }
//        public async Task<PagamentoExisteResponse> PagamentoExiste(PagamentoExisteRequest pagamento)
//        {
//            var param = new { CD_CRC = pagamento.CodigoCliente, DC_ID_PAGAMENTO = pagamento.IdentificacaoPagamento };
//            return await QueryFirstOrDefaultAsync<PagamentoExisteResponse>(SqlProc.BuscaPagamento_Proc, param, CommandType.StoredProcedure);
//        }

//        public async Task<SituacaoPagamentoResponse> SituacaoProtocolo(string protocolo)
//        {
//            var param = new { DC_LOGIN = _antiCSRFService.Login, GD_PROTOCOLO = System.Guid.Parse(protocolo).ToString() };
//            return await QueryFirstOrDefaultAsync<SituacaoPagamentoResponse>(SqlProc.BuscaPagamentoPorProtocolo_Proc, param, CommandType.StoredProcedure);
//        }

//        public async Task<SituacaoPagamentoResponse> SituacaoIdentificador(string identificadorPagamento)
//        {
//            var param = new { DC_LOGIN = _antiCSRFService.Login, DC_ID_PAGAMENTO = identificadorPagamento };
//            return await QueryFirstOrDefaultAsync<SituacaoPagamentoResponse>(SqlProc.BuscaPagamentoPorIdentificador_Proc, param, CommandType.StoredProcedure);
//        }
//    }
//}
