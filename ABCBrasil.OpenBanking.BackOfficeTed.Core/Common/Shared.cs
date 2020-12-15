using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Pagamento;
using System;
using System.Text.RegularExpressions;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Common
{
    public static class Shared
    {
        public static bool ValidaCnpjCpf(string doc)
        {
            if (string.IsNullOrWhiteSpace(doc))
                return false;

            doc = String.Join("", Regex.Split(doc, @"[^\d]"));

            if (doc.Length == 11)
                return ABCBrasil.Core.Common.Document.IsValidCpf(doc.Substring(doc.Length - 11, 11));
            else if (doc.Length == 14)
                return ABCBrasil.Core.Common.Document.IsValidCnpj(doc.Substring(doc.Length - 14, 14));
            else
                return false;
        }
        public static bool ValidaEstruturaCodigoPagamento(string codigoPagamento)
        {
            if (string.IsNullOrWhiteSpace(codigoPagamento))
                return false;

            if (codigoPagamento.Length < 44 || codigoPagamento.Length > 47)
                return false;

            return true;
        }
        public static bool ValidaCodigoPagamento(string codigoPagamento)
        {
            if (!ValidaEstruturaCodigoPagamento(codigoPagamento))
                return false;

            try
            {
               
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool ValidaTricon(string codigoPagamento)
        {
            if (!ValidaEstruturaCodigoPagamento(codigoPagamento))
                return false;

            try
            {
                
                return false;
            }
            catch
            {
                return false;
            }

        }
        public static bool ValidaValorTriCon(IncluirPagamentoRequest pagamento)
        {
           
            return false;
        }

        public static bool IsGuid(this string value)
        {
            Guid noNeed;
            return Guid.TryParse(value, out noNeed);
        }
        public static class Configuration
        {
            public const string CACHE_MAIN_KEY = "urn:OB:";
            public const string CACHE_CIP_KEY = "TITULO_CIP:";
            public const string ABC_CS_NAME = "ABC_OB_BackOfficeTed";
            public const string ABC_API = "DB_ABCAPI";
            public const string ABC_IB = "db_ib";
        }
    }
}
