using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.API.Pagamento.Core.Entities.Model;
using ABCBrasil.OpenBanking.Pagamento.Core.Common;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.Calculo;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Common
{
    public interface IPagamentoValida
    {
        void SetNoticationHandle(INotificationHandler notificationHandler);
        void SetTraceHandle(ITraceHandler traceHandler);
        void SetConfig(PagamentoConfig pagamentoConfig);
        Task<Models.Pagamento> ValidaDataHoraProcessamento(Models.Pagamento pagamento);
        bool AplicaValidacoesTitulo(Dda0110R1 xmlCip);
        bool ValidaTipoAutenticacaoValorDivergente(Models.Pagamento pagamento, CalculaTituloResponse retornoCalculo);
    }
}
