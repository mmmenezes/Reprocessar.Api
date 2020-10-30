using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.Calendario
{
    public class DiaUtilRequest
    {
        public string Moeda { get; set; }
        public string TipoFeriado { get; set; }
        public DateTime Data { get; set; }
        public string Cd_Canal { get; set; }
        public int Id_Transacao { get; set; }
    }
}
