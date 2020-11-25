using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class TedInfo
    {
        public string Gw_Evento_Api { get; set; }
        public string Cd_Evento_Api { get; set; }

        public string Dc_Payload_Request { get; set; }

        public bool Status { get; set; }


        //public string Dt_envio { get; set; }

        //public bool Fl_enviado { get; set; }



    }
}


