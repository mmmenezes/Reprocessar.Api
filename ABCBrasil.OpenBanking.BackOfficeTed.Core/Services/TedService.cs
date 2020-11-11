using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;
namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class TedService : ITedService
    {
        public TedService(ITedRepository tedRepository)
        {
            _tedRepository = tedRepository;
        }
        readonly ITedRepository _tedRepository;
        public string BuscaTeds(BuscaTedRequest tedRequest)
        {
            var teds = _tedRepository.BuscaEInsereTeds(tedRequest);
            return teds.ToCsv();
        }


    }
}
