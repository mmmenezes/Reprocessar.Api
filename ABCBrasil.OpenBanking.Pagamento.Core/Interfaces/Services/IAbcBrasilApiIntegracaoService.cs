﻿using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.Calculo;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.Calendario;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CoreComprovante;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CorePagamento;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.Tibco;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.Cip;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Comprovante;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Pagamento;
using System;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Services
{
    public interface IAbcBrasilApiIntegracaoService
    {
        void SetNoticationHandle(INotificationHandler notificationHandler);
        void SetTraceHandle(ITraceHandler traceHandler);
        Task<IServiceResult<DateTime>> ObterProximoDiaUtil(DateTime data, string correlationId);

        Task<DiaUtilResponse> ConsultarDiaUtil(Models.Pagamento pagamento);
        Task<DataExcecaoResponse> ConsultarDataExcecao(Models.Pagamento pagamento);
        Task<HoraTransacaoResponse> ConsultarHoraTransacao(Models.Pagamento pagamento);
        Task<BoletoCipResult> ObterBoletoAsync(string codigoDeBarras);
        Task<CalculaTituloResponse> CalcularTituloAsync(Models.Pagamento pagamento);
        Task<bool> IncluirPagamentoTibcoAsync(IncluirPagamentoTibcoRequest pagamento);
        Task<CoreComprovanteResponse> ObterComprovante(string protocolo);
    }
}
