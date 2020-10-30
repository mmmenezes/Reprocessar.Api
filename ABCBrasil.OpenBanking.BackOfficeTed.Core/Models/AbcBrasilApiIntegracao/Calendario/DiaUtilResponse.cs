using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.Calendario
{
    public class DiaUtilResponse
    {
        public bool Success { get; set; }
        public bool Has_information_notification { get; set; }
        public bool Has_validation_error_notification { get; set; }
        public bool Has_internal_exception_notification { get; set; }
        public DiaUtilResponseData Data { get; set; }
    }

    public class DiaUtilResponseData
    {
        public bool DiaHoraEncontrada { get; set; }
        public DateTime DiaRecebido { get; set; }
        public string Cd_Canal { get; set; }
        public int Id_Transacao { get; set; }
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
    }
}
