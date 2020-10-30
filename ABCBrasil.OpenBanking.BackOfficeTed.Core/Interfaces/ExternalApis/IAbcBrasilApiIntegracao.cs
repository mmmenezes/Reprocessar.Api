using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.Calculo;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.Calendario;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.CoreComprovante;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.Tibco;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Cip;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Comprovante;
using System;
using System.Threading.Tasks;
namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.ExternalApis
{
    public interface IAbcBrasilApiIntegracao
    {
        void SetConfig(AbcBrasilApiSettings abcBrasilApiSettings);

        Task<DiaUtilResponse> ConsultarDiaUtil(DiaUtilRequest dataHora, string protocolo);
        Task<DataExcecaoResponse> ConsultarDataExcecao(DataExcecaoRequest dataExcecao, string protocolo);
        Task<HoraTransacaoResponse> ConsultarHoraTransacao(Core.Models.Pagamento pagamento);
        Task<AbcServiceResult<DateResult>> ObterProximoDiaUtil(DateTime data, string correlationId);
        Task<BoletoCipResult> ObterBoletoAsync(string codigoDeBarras, string correlationId);
        Task<CalculaTituloResponse> CalcularTituloAsync(CalculaTituloRequest calcularRequest);
        Task<bool> IncluirPagamentoTibcoAsync(IncluirPagamentoTibcoRequest pagamento);
        Task<CoreComprovanteResponse> CoreObterComprovante(ComprovanteRequest protocolo);
    }
}
