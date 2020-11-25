using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.ReProcessaTed
{
    public class ReProcessaTed
    {
        public ReProcessaTed()
        {
            teds = new List<TedInfo>();
        }

        public int quantidadeTotal { get; set; }
       public int quantidadeFalha { get; set; }
        public List<TedInfo> teds { get; set; }

    }
}
