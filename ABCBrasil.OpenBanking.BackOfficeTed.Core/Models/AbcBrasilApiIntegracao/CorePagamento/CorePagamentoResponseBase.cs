using System;
using System.Collections.Generic;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.CorePagamento
{
    public class CorePagamentoResponseBase<T>
    {
        public CorePagamentoResponseBase()
        {
            Infos = new List<CorePagamentoResponseBaseInfos>();
            Errors = new List<CorePagamentoResponseBaseErrors>();
        }

        public bool Status { get; set; }
        public string Name { get; set; }
        public string EnvironmentName { get; set; }
        public DateTime Date { get; set; }
        public T Data { get; set; }
        public List<CorePagamentoResponseBaseInfos> Infos { get; set; }
        public List<CorePagamentoResponseBaseErrors> Errors { get; set; }
    }

    public class CorePagamentoResponseBaseInfos
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class CorePagamentoResponseBaseErrors
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
