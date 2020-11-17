﻿using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class TedService : ITedService
    {
        public TedService(IEventoRepository tedRepository)
        {
            _tedRepository = tedRepository;
        }
        readonly IEventoRepository _tedRepository;
        public string BuscaTeds(BuscaTedRequest tedRequest)
        {
            var teds = _tedRepository.BuscaEInsereTeds(tedRequest);
            return teds.ToCsv();
        }

        public bool ProcessaTed(string SelectedCSV)
        {
            var selecionadas = SelectedCSV.FromCsv<List<TedInfo>>();

            List<TransferenciaModel> transferencias = new List<TransferenciaModel>();
            foreach (var item in selecionadas)
            {
                transferencias.Add(JsonSerializer.Deserialize<TransferenciaModel>(item.Dc_Payload_Request));
            }

            //Metodo que vai ao IB2008

            //Metodo que atualiza a tabela

            //Metodo cleanup

            return false;


        }

    }
}
