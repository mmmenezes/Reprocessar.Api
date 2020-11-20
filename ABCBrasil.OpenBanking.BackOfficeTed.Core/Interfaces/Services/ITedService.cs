using System;
using System.Collections.Generic;
using System.Text;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services
{
    public interface ITedService
    {
        BuscaTedsResponse BuscaTeds(BuscaTedRequest tedRequest);
        bool ProcessaTed(string SelectedCSV);

        
    }
}