using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Comprovante
{
    public class ComprovanteRequest
    {
        public ComprovanteRequest()
        {
            id = new List<string>();
        }
        [FromRoute]
        public List<string> id { get; set; }
        public int tipoexportacao { get; set; }
    }
}
