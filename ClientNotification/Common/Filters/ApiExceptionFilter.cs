using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System;
using ClientNotification.Common.Models;
using ClientNotification.Common.Exceptions;
using ClientNotification.Common.Extensions;

namespace ClientNotification.Common.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            ApiError apiError;
            var ex = GetContextException(context);
            var httpContext = context.HttpContext;
            if (ex is UnauthorizedAccessException)
                apiError = new ApiError(HttpStatusCode.Unauthorized);
            else if (ex is BaseApiException baseException)
            {
                apiError = new ApiError(baseException.StatusCode)
                {
                    Message = baseException.Message
                };
            }
            else
            {
                apiError = new ApiError(HttpStatusCode.InternalServerError);
            }

            apiError.Path = httpContext.Request.Path;
            apiError.InitErrorText();

            logger.LogWarning(ex, apiError.Error);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = apiError.Status;
            context.Result = new JsonResult(apiError);
        }

        private static Exception GetContextException(ExceptionContext context)
        {
            var ex = context.Exception;
            return ex;
        }
    }

}
