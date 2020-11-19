namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class NotificationBase
    {
        public NotificationBase() => Type = NotificationType.Information;
        public string Code { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
    }
    public enum NotificationType { Information, Error }
}
