using System;
using System.Collections.Generic;
using System.Text;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository
{
    public class BuscaTedRequest
    {
        public DateTime DTINI { get; set; }
        public DateTime DTFIM { get; set; }
        public int QTD { get; set; }
    }
}
