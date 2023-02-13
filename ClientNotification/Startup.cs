using ClientNotification.Application.Extensions;
using ClientNotification.Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace ClientNotification
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var currentAssembly = typeof(Startup).Assembly;

            services.AddCommonDbContext(options =>
            {
                options.ConfigurationAssembly = currentAssembly;
                options.ConnectionString = Configuration.GetEnvString("EnvKeys:Postgresql");
                //options.LogOptions = new DBLogOptions()
                //{
                //    LogAction = Console.WriteLine,
                //    LogLevel = LogLevel.Debug
                //};
            });

            services.AddRepositories(currentAssembly);
            services.AddMediatR(typeof(Startup));

            services.AddControllers()
                    .AddJsonOptions(opt => {
                        var jsonOptions = opt.JsonSerializerOptions;
                        jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    });

            services.InitValidationErrorResult();
            services.AddSwaggerGen();

            services.AddNotifyService((options) => {
                options.APIKey = Configuration.GetEnvString("EnvKeys:SendGridApiKey");
                options.FromUser = Configuration.GetEnvString("EnvKeys:SGUserName");
                options.FromEMail = Configuration.GetEnvString("EnvKeys:SGUserEmail");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                /* https://localhost:<port>/swagger */
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
