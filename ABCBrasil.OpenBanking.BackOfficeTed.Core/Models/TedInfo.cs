using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class TedInfo
    {
        public Guid Gw_Evento_Api { get; set; }
        public int Cd_Evento_Api { get; set; }
        public string Dc_Payload_Request { get; set; }

    
      

    }
}
