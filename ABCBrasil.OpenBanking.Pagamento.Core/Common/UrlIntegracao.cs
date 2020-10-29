namespace ABCBrasil.OpenBanking.Pagamento.Core.Common
{
    public static class UrlIntegracao
    {
        public const string PAHT_CALENDARIO = "/dataferiados/proximo-dia-util/";
        public const string PAHT_BOLETO_CIP = "/pagamentos/boletos/";
        public const string ENDPOINT_ABRELOTE = "/Pagamento/Lote/Criar/";
        public const string ENDPOINT_ENVIALOTE = "/Pagamento/Lote/Adicionar/";
        public const string ENDPOINT_FECHALOTE = "/Pagamento/Lote/Fechar/";
        public const string ENDPOINT_DIAUTIL = "/dataferiados/verificadatahora/";
        public const string ENDPOINT_DATAEXCECAO = "/dataexcecao/verificadataexcecao/";
        public const string ENDPOINT_TIBCO = "/pagamento/";
        public const string ENDPOINT_CALCULO = "/pagamentos/boletos/{0}/calculos/valor/";
        public const string ENDPOINT_HORATRANSACAO = "/horas/horatransacional/{0}";

        public const string ENDPOINT_COMPROVANTE = "/comprovantes/geracao-comprovante-emlote/";

        public const string NOTIFICACAO_CALLBACK = @"/Notificacao";
    }
}
