
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Middleware
{
    public class ExceptionMiddleware
    {
        readonly RequestDelegate _next;
       // readonly ILogService _logService;
        readonly IApiIssuer _apiIssuer;

        //public ExceptionMiddleware(RequestDelegate next, ILogService logService, IApiIssuer apiIssuer)
        //{
        //    _next = next;
        //    _logService = logService;
        //    _apiIssuer = apiIssuer;
        //}

        //public async Task InvokeAsync(HttpContext httpContext)
        //{
        //    try
        //    {
        //        await _next(httpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logService.Write(new IssueDetail
        //        {
        //            Id = _apiIssuer.Maker(Issues.me4001),
        //            Message = ex.Message,
        //            Level = IssueLevelEnum.Error,
        //            Exception = ex
        //        }, _apiIssuer.Prefix);
        //        await HandleExceptionAsync(httpContext);
        //    }
        //}
        private Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(new ApiResult(AppDomain.CurrentDomain.FriendlyName)
            {
                Status = false,
                Date = DateTime.Now,
                Errors = new List<BasicEntity>
                {
                    new BasicEntity { Code = StatusCodes.Status500InternalServerError.ToString(), Message = "Internal Server Error." }
                }
            }.ToString());
        }
    }
}
