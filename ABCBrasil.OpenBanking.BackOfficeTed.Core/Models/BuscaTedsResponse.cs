
using System.Collections.Generic;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class BuscaTedsResponse
    {
        public IEnumerable<TedInfo> Teds { get; set; }
  
        public byte[]  CSVByte { get; set; }

    }
}
