using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class BuscaTedsResponse
    {
        public IEnumerable<TedInfo> Teds { get; set; }
  
        public byte[]  CSVByte { get; set; }

    }
}
