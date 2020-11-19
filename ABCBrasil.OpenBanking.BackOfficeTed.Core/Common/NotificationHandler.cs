using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Common
{
    public class NotificationHandler : INotificationHandler
    {
        public IList<NotificationBase> Notifications { get; set; }

        public void Add(NotificationBase notification)
        {
            if (Notifications is null) Notifications = new List<NotificationBase>();
            Notifications.Add(notification);
        }

        public void Clear()
        {
            Notifications?.Clear();
        }

        public bool IsError()
        {
            if (Notifications is null) return false;

            return Notifications.Any(n => n.Type == NotificationType.Error);
        }
    }
}
