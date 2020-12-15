
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services
{
    public interface IRelatorioService
    {        
        public Task<FileStreamResult> GerarArquivo(string scheme, HostString host, string FileName);
        public Task<object> ProcessarDados(DateTime dtini, DateTime dtfim, int cdcliente);


    }
}