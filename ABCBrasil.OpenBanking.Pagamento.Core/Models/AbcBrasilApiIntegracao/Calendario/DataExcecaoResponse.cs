using System;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.Calendario
{
    public class DataExcecaoResponse
    {
        public bool Success { get; set; }
        public bool Has_information_notification { get; set; }
        public bool Has_validation_error_notification { get; set; }
        public bool Has_internal_exception_notification { get; set; }
        public DataExcecaoResponseData Data { get; set; }
    }

    public class DataExcecaoResponseData
    {
        public int Id_DataExcecao { get; set; }
        public DateTime Dc_DiaMesAno { get; set; }
        public string Cd_Canal { get; set; }
        public string Cd_Transacao { get; set; }
        public int Id_Transacao { get; set; }
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
        public bool Fl_Ativo { get; set; }
    }
}
