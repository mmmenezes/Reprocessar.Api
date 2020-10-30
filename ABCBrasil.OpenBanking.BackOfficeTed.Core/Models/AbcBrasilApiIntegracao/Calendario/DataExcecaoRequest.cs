using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.Calendario
{
    public class DataExcecaoRequest
    {
        public DateTime Dc_DiaMesAno { get; set; }
        public string Cd_Canal { get; set; }
        public int Id_Transacao { get; set; }
    }
}
