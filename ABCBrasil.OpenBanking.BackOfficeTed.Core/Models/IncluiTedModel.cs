using System;
using System.Collections.Generic;
using System.Text;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class IncluiTedModel
    {
        public int CD_TIPO_CLIENTE { get; set; }
        public int CD_USUARIO { get; set; }
        public string ID_TRANSACAO { get; set; }
        public string CD_AGENCIA_DEBITO { get; set; }
        public string DC_CONTA_DEBITO { get; set; }
        public double VL_VALOR { get; set; }
        public bool FL_APROVADO { get; set; }
        public string CD_FINALIDADE { get; set; }
        public string CD_BANCO_CRED { get; set; }
        public string CD_AGENCIA_CRED { get; set; }
        public string DC_CONTA_CRED { get; set; }
        public string CD_TIPO_CONTA_CRED { get; set; }
        public string CD_CNPJ_CPF_CLI_CRED { get; set; }
        public string DC_NOME_CLI_CRED { get; set; }
        public string CD_CNPJ_CPFCLI_CRED2 { get; set; }
        public string DC_NOME_CLI_CRED2 { get; set; }
        public DateTime DT_TRANSFERENCIA { get; set; }
        public string CD_PROTOCOLO_API { get; set; }
        public string DC_URL_CALLBACK { get; set; }
        public string CD_TED_CLIENTE { get; set; }
    }
}
