using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;


namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected readonly IApiIssuer Issuer;
        protected readonly INotificationHandler NotificationHandler;
        protected readonly ITraceHandler TraceHandler;

        public ApiController(IApiIssuer issuer, INotificationHandler notificationHandler, ITraceHandler traceHandler)
        {
            Issuer = issuer;
            NotificationHandler = notificationHandler;
            TraceHandler = traceHandler;
        }
        /// <summary>
        /// Response:Metodo padrão para o Retorno ao Cliente.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result">TResult</param>
        /// <param name="httpStatusCode">HttpStatusCode</param>
        /// <returns></returns>
        protected new IActionResult Response<TResult>(TResult result, HttpStatusCode httpStatusCode)
        {
            var apiResult = new ApiResult<TResult>(System.AppDomain.CurrentDomain.FriendlyName)
            {
                Data = result,
                Status = !NotificationHandler.IsError()
            };

            apiResult.AddErrors(
                    NotificationHandler?.Notifications?
                    .Where(o => o.Type == NotificationType.Error && !string.IsNullOrEmpty(o.Code) && !string.IsNullOrEmpty(o.Message))
                    .Select(o => new BasicEntity { Code = o.Code, Message = o.Message })
                    .OrderBy(o => o.Code)
                );

            apiResult.AddInfos(
                    NotificationHandler?.Notifications?
                    .Where(o => o.Type != NotificationType.Error && !string.IsNullOrEmpty(o.Code) && !string.IsNullOrEmpty(o.Message))
                    .Select(o => new BasicEntity { Code = o.Code, Message = o.Message })
                    .OrderBy(o => o.Code)
                );

            return Response(apiResult, httpStatusCode);
        }



        /// <summary>
        /// Response:Metodo padrão para o Retorno ao Cliente.
        /// </summary>
        /// <typeparam name="TResult">TResult</typeparam>
        /// <param name="apiResult">ApiResult</param>
        /// <param name="httpStatusCode">HttpStatusCode</param>
        /// <returns></returns>
        protected new IActionResult Response<TResult>(ApiResult<TResult> apiResult, HttpStatusCode httpStatusCode)
            => StatusCode((int)httpStatusCode, apiResult);

        /// <summary>
        /// AddNotification: Envia uma Notificação no response do Cliente usando um Issue.
        /// </summary>
        /// <param name="issue">Issues</param>
        /// <param name="message">String</param>
        /// <param name="notificationType">NotificationType</param>
        protected void AddNotification(Issues issue, string message, NotificationType notificationType) =>
            AddNotification(Issuer.MakerCode(issue), message, notificationType);

        /// <summary>
        /// AddNotification: Envia uma Notificação no response do Cliente.
        /// </summary>
        /// <param name="code">String</param>
        /// <param name="message">String</param>
        /// <param name="notificationType">NotificationType</param>
        protected void AddNotification(string code, string message, NotificationType notificationType) =>
            NotificationHandler.Add(new NotificationBase { Code = code, Message = message, Type = notificationType });

        /// <summary>
        /// AddTrace: Envia informação para o Log
        /// </summary>
        /// <param name="message">String</param>
        /// <param name="informationData">object</param>
        protected void AddTrace(string message, object informationData = null) =>
            TraceHandler.AddTrace(new TraceInfo { Message = message, InformationData = informationData }, Issuer.Prefix);

        /// <summary>
        /// AddError:Envia o Erro para o Log e no response do Cliente.
        /// </summary>
        /// <param name="issue">Issues</param>
        /// <param name="message">String</param>
        /// <param name="ex">Exception</param>
        protected void AddError(Issues issue, string message, Exception ex)
        {
            AddNotification(issue, message, NotificationType.Error);
            TraceHandler.AddTrace(new TraceInfo { Code = Issuer.Maker(issue), Message = message, Level = TraceLevel.Error, Exception = ex });
        }

             
    }
}
