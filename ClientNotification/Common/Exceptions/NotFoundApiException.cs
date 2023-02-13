using System.Net;

namespace ClientNotification.Common.Exceptions
{
    internal class NotFoundApiException : BaseApiException
    {
        public NotFoundApiException(string message)
            : base(message, HttpStatusCode.NotFound)
        {
        }
    }
}
