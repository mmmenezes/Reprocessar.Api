using ABCBrasil.Core.Data;
using ABCBrasil.OpenBanking.Pagamento.Core.Common;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CallbackCore;
using Dapper;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Infra.Repository
{
    public class CallbackCoreRepository : DapperRepository, ICallbackCoreRepository
    {
        public CallbackCoreRepository(IConnectDataBase connectDataBase) : base(connectDataBase)
        {
        }

        public async Task<CallbackCoreResponse> AtualizarSituacaoPagamento(Core.Models.SituacaoPagamento situacaoPagamento)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(name: "GD_PROTOCOLO", value: situacaoPagamento.Protocolo, dbType: System.Data.DbType.Guid);
            parameters.Add(name: "CD_PAGAMENTO_SITUACAO", value: situacaoPagamento.Situacao, dbType: System.Data.DbType.Int32);
            parameters.Add(name: "CD_RETORNO", value: situacaoPagamento.Retorno, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.InputOutput);
            parameters.Add(name: "DC_MENSAGEM_RETORNO", value: situacaoPagamento.Mensagem, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.InputOutput, size: 500);
            await ExecuteAsync(SqlProc.UpdateSituacaoPagamentoCallbackCore_Proc, parameters, System.Data.CommandType.StoredProcedure);

            return new CallbackCoreResponse
            {
                CodigoRetorno = parameters.Get<int>("CD_RETORNO"),
                MensagemRetorno = parameters.Get<string>("DC_MENSAGEM_RETORNO")
            };
        }
    }
}
