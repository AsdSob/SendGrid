using Microsoft.Extensions.Configuration;
using System;

namespace ClientNotification.Common.Extensions
{
    public static class ConfigEnvironmentExtensions
    {
        public static string GetEnvString(this IConfiguration configuration, string key, bool checkEmpty = true)
        {
            var envKey = configuration.GetValue<string>(key);
            var value = Environment.GetEnvironmentVariable(envKey);
            if (checkEmpty && string.IsNullOrEmpty(value))
                throw new Exception($"Environment with name '{envKey}' is empty");
            return value;
        }
    }
}
