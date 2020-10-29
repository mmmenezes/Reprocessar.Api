namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CorePagamento
{
    public class CorePagamentoResponse<T> : CorePagamentoResponseBase<T>
    {
        
    }

    public class CorePagamentoAbrirLoteDataResponse
    {
        public string Protocolo { get; set; }
    }

    public class CorePagamentoEnviarLoteDataResponse
    {
        public string Protocolo { get; set; }
    }

    public class CorePagamentoFecharLoteDataResponse
    {
        public string Data { get; set; }
    }

}
