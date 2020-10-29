using System.Collections.Generic;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CorePagamento
{
    public class CorePagamentoAbrirLoteRequest
    {
        public CorePagamentoAbrirLoteCallback Callback { get; set; }
        public int Canal { get; set; }
    }

    public class CorePagamentoAbrirLoteCallback
    {
        public string Url { get; set; }
        public string ApiKeyName { get; set; }
        public string ApiKeyValue { get; set; }
    }

    public class CorePagamentoEnviarLoteRequest
    {
        public CorePagamentoEnviarLoteRequest()
        {
            Pagamentos = new List<CorePagamentoEnviarLotePagamento>();
        }

        public string Protocolo { get; set; }
        public List<CorePagamentoEnviarLotePagamento> Pagamentos { get; set; }
    }

    public class CorePagamentoEnviarLotePagamento
    {
        public string Coligada { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string CodigoBarras { get; set; }
        public decimal? Valor { get; set; }
        public string NumeroDocumento { get; set; }
        public string Identificador { get; set; }
        public int Crc { get; set; }
    }

    public class CorePagamentoFecharLoteRequest
    {
        public string Protocolo { get; set; }
    }
}
