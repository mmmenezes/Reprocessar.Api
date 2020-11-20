﻿using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository
{
    public interface IEventoRepository
    {
        Task<IEnumerable<TedInfo>> BuscaTeds(BuscaTedRequest request);
        Task<bool> InsereTeds(TedInfo ted);
        Task<bool> AtualizaEnvio(int Cd_Evento_Api);
    }
}
