using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Pagamento;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository
{
    public class PagamentoExiste
    {
        public bool Sucesso { get; set; }
        public bool IdExiste { get; set; }
        public IncluirPagamentoResponse IncluirPagamentoResponse { get; set; }
    }
}
