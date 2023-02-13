using ClientNotification.Common.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace ClientNotification.Common.Extensions
{
    internal static class ApiErrorExtensions
    {
        public static void InitErrorText(this ApiError apiError)
        {
            switch ((HttpStatusCode)apiError.Status)
            {
                case HttpStatusCode.BadRequest:
                    {
                        apiError.Error = "Bad Request";
                        break;
                    }
                case HttpStatusCode.Conflict:
                    {
                        apiError.Error = "Conflict";
                        break;
                    }
                case HttpStatusCode.NotFound:
                    {
                        apiError.Error = "Not Found";
                        break;
                    }
                case HttpStatusCode.Unauthorized:
                    {
                        apiError.Error = "Unauthorized";
                        break;
                    }
                case HttpStatusCode.Forbidden:
                    {
                        apiError.Error = "Forbidden";
                        break;
                    }
                case HttpStatusCode.InternalServerError:
                default:
                    {
                        apiError.Error = "Internal Server Error";
                        break;
                    }
            }
        }

        public static async Task WriteJsonApiErrorAsync(this HttpContext httpContext, ApiError apiError)
        {
            apiError.Path = httpContext.Request.Path;
            apiError.InitErrorText();

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = apiError.Status;
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(apiError));
        }
    }
}
