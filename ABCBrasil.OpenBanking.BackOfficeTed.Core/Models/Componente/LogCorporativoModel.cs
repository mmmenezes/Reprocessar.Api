
using ABCBrasil.Core.LogCorporativo.Lib;
namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Componente
{
    public class LogCorporativoModel
    {
        public string Acao { get; set; }
        public string Observacao { get; set; }
        public LogCategoria CodigoCategoria { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoOperacao { get; set; }
    }
}
