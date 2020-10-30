using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.API.Pagamento.Core.Entities.Model;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.Calculo;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Common
{
    public interface IPagamentoValida
    {
        void SetNoticationHandle(INotificationHandler notificationHandler);
        void SetTraceHandle(ITraceHandler traceHandler);
        void SetConfig(PagamentoConfig pagamentoConfig);
        Task<Models.Pagamento> ValidaDataHoraProcessamento(Models.Pagamento pagamento);
        bool AplicaValidacoesTitulo(Dda0110R1 xmlCip);
        //bool ValidaTipoAutenticacaoValorDivergente(Models.Pagamento pagamento, CalculaTituloResponse retornoCalculo);
    }
}
