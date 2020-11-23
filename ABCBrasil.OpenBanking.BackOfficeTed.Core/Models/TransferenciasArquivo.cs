using System;
using System.Collections.Generic;
using System.Text;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class TransferenciasArquivo
    {
        public TransferenciasArquivo()
        {
            transferencia = new TransferenciaModel();
            root   = new Root();
            callback = new Callback();
        }
               

        public class Root
        {
            public int Codigo { get; set; }
            public string Protocolo { get; set; }
            
        }

        public Root root { get; set; }
        public TransferenciaModel transferencia { get; set; }
        public Callback callback { get; set; }

        public class Callback
        {
            public string Url { get; set; }
        }
    }
}
