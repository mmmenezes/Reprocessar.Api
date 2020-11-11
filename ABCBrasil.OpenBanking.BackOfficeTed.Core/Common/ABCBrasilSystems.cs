using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Common
{
    public enum ABCBrasilSystems
    {
        [Description("None")]
        None = 0,

        [Description("IBPJ")]
        InternetBanking = 1,

        [Description("OBPJ")]
        OpenBanking = 2,

        [Description("IBPF")]
        InternetBankingPF = 3,

        [Description("Core")]
        Core = 4,
    }
}
