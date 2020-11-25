using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class ServiceBase
    {
        INotificationHandler _notificationHandler;
        ITraceHandler _traceHandler;

        readonly IApiIssuer _issuer;

        public ServiceBase(IApiIssuer issuer) => _issuer = issuer;
        
        public virtual void SetNoticationHandle(INotificationHandler notificationHandler) =>
            _notificationHandler = notificationHandler;

        public virtual void SetTraceHandle(ITraceHandler traceHandler) =>
            _traceHandler = traceHandler;

        protected virtual void AddTrace(string message, object informationData = null) =>
            _traceHandler?.AddTrace(new TraceInfo { Message = message, InformationData = informationData }, _issuer.Prefix);

        protected virtual void AddTrace(Issues issue, string message, Exception ex) =>
            _traceHandler?.AddTrace(new TraceInfo { Code = _issuer.Maker(issue), Message = message, Level = TraceLevel.Error, Exception = ex }, _issuer.Prefix);

        protected virtual void AddWarning(Issues issue, string message)
        {
            _notificationHandler?.Add(new NotificationBase { Code = _issuer.MakerCode(issue), Message = message });
            _traceHandler?.AddTrace(new TraceInfo { Code = _issuer.Maker(issue), Message = message, Level = TraceLevel.Warning }, _issuer.Prefix);
        }

        protected virtual void AddError(Issues issue, string message, Exception ex)
        {
            _notificationHandler?.Add(new NotificationBase { Code = _issuer.MakerCode(issue), Message = message, Type = NotificationType.Error });
            _traceHandler?.AddTrace(new TraceInfo { Code = _issuer.Maker(issue), Message = message, Level = TraceLevel.Error, Exception = ex }, _issuer.Prefix);
        }

        protected virtual void AddNotification(Issues issue, string message, NotificationType notificationType)
        {
            _notificationHandler?.Add(new NotificationBase { Code = _issuer.MakerCode(issue), Message = message, Type = notificationType });
            _traceHandler?.AddTrace(new TraceInfo { Code = _issuer.Maker(issue), Message = message, Level = TraceLevel.Error }, _issuer.Prefix);
        }

        protected virtual string GetCorrelation()
        {
            return _traceHandler.CorrelationId;
        }
    }
}
