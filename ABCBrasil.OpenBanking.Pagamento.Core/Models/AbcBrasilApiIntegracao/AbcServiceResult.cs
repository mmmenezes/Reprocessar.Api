using System.Collections.Generic;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao
{
    public class AbcServiceResult<T>
    {
        public bool Success { get; set; }
        public bool Has_information_notification { get; set; }
        public bool Has_validation_error_notification { get; set; }
        public bool Has_internal_exception_notification { get; set; }
        public IList<NotificationBase> Notifications { get; set; }
        public T Data { get; set; }
    }

    public class NotificationBase
    {
        public string Message { get; set; }
        public string Type { get; set; }
    }

    public class Data
    {
        public string Date { get; set; }
        public string DayOfWeek { get; set; }
    }
}
