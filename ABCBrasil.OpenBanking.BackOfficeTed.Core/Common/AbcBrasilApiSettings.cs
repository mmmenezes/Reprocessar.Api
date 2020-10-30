namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Common
{
    public class AbcBrasilApiSettings
    {
        public string ApiName { get; set; }
        public bool CacheActivated { get; set; }
        public int CacheTtl { get; set; }
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
        public CalendarioConfig CalendarioConfig { get; set; }
        public CoreCalculoConfig CoreCalculoConfig { get; set; }
        public CorePagamentoLoteConfig CorePagamentoLoteConfig { get; set; }
        public CoreComprovanteConfig CoreComprovanteConfig { get; set; }
        public TibcoPagamentoConfig TibcoPagamentoConfig { get; set; }
        public TibcoNotificacaoConfig TibcoNotificacaoConfig { get; set; }
    }
}
