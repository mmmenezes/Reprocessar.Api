using System;
using System.Collections.Generic;
using System.Text;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class TransferenciaInclui
    {
        public string CdTipoCliente { get; set; }
        public int CdUsuario { get; set; }
        public string IdTransacao { get; set; }
        public string CdAgenciaDebito { get; set; }
        public string DcContaDebito { get; set; }
        public double VlValor { get; set; }
        public bool FlAprovado { get; set; }
        public string CdFinalidade { get; set; }
        public string CdBancoCred { get; set; }
        public string CdAgenciaCred { get; set; }
        public string DcContaCred { get; set; }
        public string CdTipoContaCred { get; set; }
        public string CdCnpjCpfCliCred { get; set; }
        public string DcNomeCliCred { get; set; }
        public string CdCnpjCpfCliCred2 { get; set; }
        public string DcNomeCliCred2 { get; set; }
        public string DtTransferencia { get; set; }
        public string CdProtocoloApi { get; set; }
        public string DcUrlCallBack { get; set; }
        public string CdTedCliente { get; set; }
        
    }
}
