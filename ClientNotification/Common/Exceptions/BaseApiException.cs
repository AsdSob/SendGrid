using System.Net;
using System;

namespace ClientNotification.Common.Exceptions
{
    public abstract class BaseApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected BaseApiException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
