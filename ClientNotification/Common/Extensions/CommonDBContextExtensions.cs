using ClientNotification.Common.Configurations;
using ClientNotification.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace ClientNotification.Common.Extensions
{
    public static class CommonDBContextExtensions
    {
        public static IServiceCollection AddCommonDbContext(
                            this IServiceCollection services,
                            Action<CommonDBContextOptions> configureOptions)
        {
            services.Configure(configureOptions);
            services.AddDbContext<AppDbContext>((provider, builder) =>
            {
                var configOptions = provider.GetRequiredService<IOptions<CommonDBContextOptions>>();
                var config = configOptions.Value;
                builder.UseNpgsql(config.ConnectionString, b => b.MigrationsAssembly(config.ConfigurationAssembly.FullName));
                if (config.LogOptions != null)
                {
                    var logOptions = config.LogOptions;
                    builder.LogTo(logOptions.LogAction, logOptions.LogLevel);
                }
            });
            services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
            return services;
        }
    }
}
