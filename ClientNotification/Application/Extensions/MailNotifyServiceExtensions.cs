using ClientNotification.Application.Configs;
using ClientNotification.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ClientNotification.Application.Extensions
{
    internal static class MailNotifyServiceExtensions
    {
        public static IServiceCollection AddNotifyService(this IServiceCollection services, Action<SendGridOptions> options)
        {
            services.Configure(options);
            services.AddSingleton<IEMailNotifyService, EMailNotifyService>();

            return services;
        }
    }
}
