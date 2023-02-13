using ClientNotification.Common.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ClientNotification.Common.Extensions
{
    public static class ApiCustomErrorResultExtensions
    {
        public static IServiceCollection InitValidationErrorResult(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                var responseFactory = new RequestApiValidationProblemDetailsResult(options.InvalidModelStateResponseFactory);
                options.InvalidModelStateResponseFactory = context => responseFactory;
            });
            return services;
        }
    }

}
