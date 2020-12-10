using System.Collections.Generic;

using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.ReProcessaTed;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services
{
    public interface ITedService
    {
        BuscaTedsResponse BuscaTeds(BuscaTedRequest tedRequest);
        ReProcessaTed ProcessaArquivoTed(IList<TransferenciasArquivo> SelectedCSV);
        public List<TransferenciasArquivo> ProcessaArquivo(UploadViewModel file);

    }
}