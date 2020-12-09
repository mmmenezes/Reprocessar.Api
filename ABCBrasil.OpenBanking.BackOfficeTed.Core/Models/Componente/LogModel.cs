namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Componente
{
    public class LogModel
    {
        public LogCorporativoModel LogCorporativoModel { get; set; } = null;
        public LogEventoModel LogEventoModel { get; set; } = null;
        public bool ValidarLogCorporativo
        {
            get
            {
                return (LogCorporativoModel != null);
            }
        }
    }
}
