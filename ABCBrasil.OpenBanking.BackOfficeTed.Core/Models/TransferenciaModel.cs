using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class TransferenciaModel
    {
        public int? CodCliente { get; set; }
        public string AgenciaCliente { get; set; }
        public string ContaCliente { get; set; }
        [MaxLength(2, ErrorMessage = "Excedeu o limite máximo de caracteres para o campo!")]
        [MinLength(2, ErrorMessage = "Quantidade de caracteres insuficiente para o campo!")]
        public string TipoContaFavorecido { get; set; }
        [MaxLength(3, ErrorMessage = "Excedeu o limite máximo de caracteres para o campo!")]
        public string BancoFavorecido { get; set; }
        public string AgenciaFavorecido { get; set; }
        public string ContaFavorecido { get; set; }
        public string NumDocumentoFavorecido { get; set; }
        [MaxLength(35, ErrorMessage = "Excedeu o limite máximo de caracteres para o campo!")]
        public string NomeFavorecido { get; set; }
        public string NumDocumentoFavorecido2 { get; set; }
        [MaxLength(35, ErrorMessage = "Excedeu o limite máximo de caracteres para o campo!")]
        public string NomeFavorecido2 { get; set; }
        public DateTime DataTransacao { get; set; }
        public string Finalidade { get; set; }
        public decimal Valor { get; set; }
    }
}
