using System;
using System.Collections.Generic;
using System.Text;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class IncluiTedModel
    {

        public int CodCliente { get; set; }
        public string AgenciaCliente { get; set; }
        public string ContaCliente { get; set; }
        public string TipoContaFavorecido { get; set; }
        public string BancoFavorecido { get; set; }
        public string AgenciaFavorecido { get; set; }
        public string ContaFavorecido { get; set; }
        public string NumDocumentoFavorecido { get; set; }
        public string NomeFavorecido { get; set; }
        public object NumDocumentoFavorecido2 { get; set; }
        public object NomeFavorecido2 { get; set; }
        public DateTime DataTransacao { get; set; }
        public string Finalidade { get; set; }
        public float Valor { get; set; }
        public Callback Callback { get; set; }
    }

    public class Callback
    {
        public string Url { get; set; }
    }
}
