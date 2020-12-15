using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository
{
    public class BuscaTedRequest
    {
        public DateTime DTINI { get; set; }
        public DateTime DTFIM { get; set; }
        public int? CDCliente { get; set; }
    }
}
