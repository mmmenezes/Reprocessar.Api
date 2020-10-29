using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ABCBrasil.OpenBanking.API.Pagamento.Core.Entities.Model
{
    [Serializable()]
    [XmlRoot("DDA0110R1")]
    public partial class Dda0110R1
    {
        public string CodMsg { get; set; }


        public string NumCtrlPart { get; set; }


        public uint ISPBPartRecbdrPrincipal { get; set; }


        public uint ISPBPartRecbdrAdmtd { get; set; }


        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string NumCtrlDDA { get; set; }


        public ulong NumIdentcTit { get; set; }


        public ulong NumRefAtlCadTit { get; set; }


        public byte NumSeqAtlzCadTit { get; set; }


        public DateTime DtHrSitTit { get; set; }


        public string ISPBPartDestinatario { get; set; }


        public string CodPartDestinatario { get; set; }


        public string TpPessoaBenfcrioOr { get; set; }


        public string CNPJ_CPFBenfcrioOr { get; set; }


        public string Nom_RzSocBenfcrioOr { get; set; }


        public string NomFantsBenfcrioOr { get; set; }


        public string CEPBenfcrioOr { get; set; }


        public string TpPessoaBenfcrioFinl { get; set; }


        public string CNPJ_CPFBenfcrioFinl { get; set; }


        public string Nom_RzSocBenfcrioFinl { get; set; }


        public string NomFantsBenfcrioFinl { get; set; }


        public string TpPessoaPagdr { get; set; }


        public string CNPJ_CPFPagdr { get; set; }


        public string Nom_RzSocPagdr { get; set; }


        public string NomFantsPagdr { get; set; }


        public string CodMoedaCNAB { get; set; }


        public string NumCodBarras { get; set; }


        public string NumLinhaDigtvl { get; set; }


        public DateTime DtVencTit { get; set; }


        public decimal VlrTit { get; set; }


        public string CodEspTit { get; set; }


        public DateTime DtLimPgtoTit { get; set; }


        public string IndrBloqPgto { get; set; }


        public string IndrPgtoParcl { get; set; }

        public int QtdPgtoParcl { get; set; }


        public decimal VlrAbattTit { get; set; }


        public Dda0110R1GrupoDda0110R1JurosTit Grupo_DDA0110R1_JurosTit { get; set; }


        public Dda0110R1GrupoDda0110R1MultaTit Grupo_DDA0110R1_MultaTit { get; set; }


        public List<Dda0110R1GrupoDda0110R1DesctTit> Grupo_DDA0110R1_DesctTit { get; set; }


        public string TpModlCalc { get; set; }


        public string TpAutcRecbtVlrDivgte { get; set; }


        public int SitTitPgto { get; set; }


        public DateTime DtHrDDA { get; set; }


        public DateTime DtMovto { get; set; }
    }

    [Serializable()]
    public partial class Dda0110R1GrupoDda0110R1JurosTit
    {
        public System.DateTime DtJurosTit { get; set; }
        public int CodJurosTit { get; set; }
        public decimal Vlr_PercJurosTit { get; set; }
    }

    [Serializable()]
    public partial class Dda0110R1GrupoDda0110R1MultaTit
    {
        public int CodMultaTit { get; set; }
        public decimal Vlr_PercMultaTit { get; set; }
    }

    [Serializable()]
    public partial class Dda0110R1GrupoDda0110R1DesctTit
    {
        public int CodDesctTit { get; set; }
        public decimal Vlr_PercDesctTit { get; set; }
    }
}