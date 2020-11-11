using System.Collections.Generic;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public interface INotificationHandler
    {
        IList<NotificationBase> Notifications { get; set; }
        void Add(NotificationBase notification);
        void Clear();
        bool IsError();
    }
}
