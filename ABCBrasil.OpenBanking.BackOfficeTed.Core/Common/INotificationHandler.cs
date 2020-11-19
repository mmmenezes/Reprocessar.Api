using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
using System.Collections.Generic;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Common
{
    public interface INotificationHandler
    {
        IList<NotificationBase> Notifications { get; set; }
        void Add(NotificationBase notification);
        void Clear();
        bool IsError();
    }
}
