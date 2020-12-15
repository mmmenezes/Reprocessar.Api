using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Mappings;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using Csv;
using System.Linq;
using AutoMapper;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.ReProcessaTed;
using Microsoft.AspNetCore.Hosting;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class RelatorioService : ServiceBase, IRelatorioService
    {
        public RelatorioService(IEventoRepository tedRepository, IIBRepository iBRepository, IMapper mapper, IApiIssuer issuer, IWebHostEnvironment webHostEnvironmen) : base(issuer)
        {
            _tedRepository = tedRepository;
            _ibRepository = iBRepository;
            _webHostEnvironmen = webHostEnvironmen;
        }
        

        readonly IEventoRepository _tedRepository;
        readonly IIBRepository _ibRepository;
        private IWebHostEnvironment _webHostEnvironmen;
        

        public async Task<FileStreamResult> GerarArquivo(string scheme, HostString host, string FileName)
        {
            string sWebRootFolder = _webHostEnvironmen.WebRootPath;
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, FileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("employee");
                IRow row = excelSheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("EmployeeId");
                row.CreateCell(1).SetCellValue("EmployeeName");
                row.CreateCell(2).SetCellValue("Age");
                row.CreateCell(3).SetCellValue("Sex");
                row.CreateCell(4).SetCellValue("Designation");
                row = excelSheet.CreateRow(1);
                row.CreateCell(0).SetCellValue(1);
                row.CreateCell(1).SetCellValue("Jack Supreu");
                row.CreateCell(2).SetCellValue(45);
                row.CreateCell(3).SetCellValue("Male");
                row.CreateCell(4).SetCellValue("Solution Architect");
                row = excelSheet.CreateRow(2);
                row.CreateCell(0).SetCellValue(2);
                row.CreateCell(1).SetCellValue("Steve khan");
                row.CreateCell(2).SetCellValue(33);
                row.CreateCell(3).SetCellValue("Male");
                row.CreateCell(4).SetCellValue("Software Engineer");
                row = excelSheet.CreateRow(3);
                row.CreateCell(0).SetCellValue(3);
                row.CreateCell(1).SetCellValue("Romi gill");
                row.CreateCell(2).SetCellValue(25);
                row.CreateCell(3).SetCellValue("FeMale");
                row.CreateCell(4).SetCellValue("Junior Consultant");
                row = excelSheet.CreateRow(4);
                row.CreateCell(0).SetCellValue(4);
                row.CreateCell(1).SetCellValue("Hider Ali");
                row.CreateCell(2).SetCellValue(34);
                row.CreateCell(3).SetCellValue("Male");
                row.CreateCell(4).SetCellValue("Accountant");
                row = excelSheet.CreateRow(5);
                row.CreateCell(0).SetCellValue(5);
                row.CreateCell(1).SetCellValue("Mathew");
                row.CreateCell(2).SetCellValue(48);
                row.CreateCell(3).SetCellValue("Male");
                row.CreateCell(4).SetCellValue("Human Resource");
                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, FileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            FileStreamResult result = new FileStreamResult(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            return result;
        }

        public Task<object> ProcessarDados(DateTime dtini, DateTime dtfim, int cdcliente)
        {
            throw new NotImplementedException();
        }
    }
}
