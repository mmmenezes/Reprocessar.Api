namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.Calendario
{
    public class HoraTransacaoResponse
    {
        public bool Success { get; set; }
        public bool Has_information_notification { get; set; }
        public bool Has_validation_error_notification { get; set; }
        public bool Has_internal_exception_notification { get; set; }
        public HoraTransacaoResponseData Data { get; set; }
    }

    public class HoraTransacaoResponseData
    {
        public string CanalCode { get; set; }
        public string TransactionCode { get; set; }
        public int TransactionId { get; set; }
        public string InitialHour { get; set; }
        public string FinalHour { get; set; }
    }
}
