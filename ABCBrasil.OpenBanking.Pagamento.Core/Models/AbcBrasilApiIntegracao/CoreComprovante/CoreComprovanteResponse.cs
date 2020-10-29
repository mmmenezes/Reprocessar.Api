using System;
using System.Collections.Generic;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CoreComprovante
{
    public class CoreComprovanteResponse
    {
        public CoreComprovanteResponse()
        {
            Data = new List<CoreComprovanteResponseDatas>();
            Infos = new List<CoreComprovanteResponseInfos>();
            Errors = new List<CoreComprovanteResponseErrors>();
        }


        public bool Status { get; set; }
        public string Name { get; set; }
        public string EnvironmentName { get; set; }
        public DateTime Date { get; set; }
        public List<CoreComprovanteResponseDatas> Data { get; set; }
        public List<CoreComprovanteResponseInfos> Infos { get; set; }
        public List<CoreComprovanteResponseErrors> Errors { get; set; }
        public class CoreComprovanteResponseDatas
        {
            public string NomeArquivo { get; set; }
            public bool Flgerado { get; set; }
            public string Mensagem { get; set; }
            public string ArquivoGerado { get; set; }
        }

        public class CoreComprovanteResponseInfos
        {
            public string Code { get; set; }
            public string Message { get; set; }
            public string Title { get; set; }
            public string Property { get; set; }
        }

        public class CoreComprovanteResponseErrors
        {
            public string Code { get; set; }
            public string Message { get; set; }
            public string Title { get; set; }
            public string Property { get; set; }
        }
    }
}
