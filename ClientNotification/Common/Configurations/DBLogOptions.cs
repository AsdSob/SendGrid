using Microsoft.Extensions.Logging;
using System;

namespace ClientNotification.Common.Configurations
{
    public class DBLogOptions
    {
        public Action<string> LogAction { get; set; }

        public LogLevel LogLevel { get; set; }
    }
}
