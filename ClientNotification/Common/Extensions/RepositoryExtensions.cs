using ClientNotification.Common.Persistence.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace ClientNotification.Common.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(
                                            this IServiceCollection services,
                                            Assembly assembly,
                                            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var repositoryType = typeof(IRepository);
            var types = assembly.GetTypes()
                                .Where(type => repositoryType.IsAssignableFrom(type) &&
                                               !type.IsInterface &&
                                               type.GetInterfaces()
                                                   .Any(x => x != repositoryType &&
                                                             repositoryType.IsAssignableFrom(x)))
                                .ToArray();
            foreach (var type in types)
            {
                var interfaceType = type.GetInterfaces()
                                        .First(type => type != repositoryType &&
                                                       repositoryType.IsAssignableFrom(type));
                var serviceDescription = new ServiceDescriptor(interfaceType, type, lifetime);
                services.Add(serviceDescription);
            }
            return services;
        }
    }
}
