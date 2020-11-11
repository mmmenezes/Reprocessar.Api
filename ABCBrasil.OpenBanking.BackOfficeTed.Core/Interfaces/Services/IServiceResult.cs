
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using System.Collections.Generic;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services
{
    public interface IServiceResult<T>
    {
        T Result { get; set; }
        bool Status { get; set; }
        string Message { get; set; }
        List<IssueDetail> Issues { get; set; }
    }
}
