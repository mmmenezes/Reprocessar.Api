using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Pagamento;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.Repository
{
    public class PagamentoExiste
    {
        public bool Sucesso { get; set; }
        public bool IdExiste { get; set; }
        public IncluirPagamentoResponse IncluirPagamentoResponse { get; set; }
    }
}
