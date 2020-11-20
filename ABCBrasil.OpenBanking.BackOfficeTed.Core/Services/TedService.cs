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

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class TedService : ITedService
    {
        public TedService(IEventoRepository tedRepository, IIBRepository iBRepository)
        {
            _tedRepository = tedRepository;
            _ibRepository = iBRepository;
        }
        readonly IEventoRepository _tedRepository;
        readonly IIBRepository _ibRepository;
        public IEnumerable<TedInfo> BuscaTeds(BuscaTedRequest tedRequest)
        {
            var teds = _tedRepository.BuscaTeds(tedRequest);
  
            var TED = teds.GetAwaiter().GetResult();
            return TED;
            
        }

        public bool ProcessaTed(string SelectedCSV)
        {
            var selecionadas = SelectedCSV.FromCsv<List<TedInfo>>();

            List<TransferenciaModel> transferencias = new List<TransferenciaModel>();
            foreach (var item in selecionadas)
            {
                _tedRepository.InsereTeds(item);
                var transferencia = item.Dc_Payload_Request.MapTo<TransferenciaInclui>();
                _ibRepository.Atualiza(transferencia);
                _tedRepository.AtualizaEnvio(Int32.Parse(item.Cd_Evento_Api));

               
            }

            return false;


        }

   
    }
}
