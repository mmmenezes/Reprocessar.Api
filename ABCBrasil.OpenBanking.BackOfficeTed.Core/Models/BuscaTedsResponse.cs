using System;
using System.Collections.Generic;
using System.Text;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class BuscaTedsResponse
    {
        public IEnumerable<TedInfo> Teds { get; set; }
        public string  CSVByte { get; set; }
    }
}
