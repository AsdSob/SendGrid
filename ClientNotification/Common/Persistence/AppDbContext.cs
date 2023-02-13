using ClientNotification.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ClientNotification.Common.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly ICommonDBContextOptions commonOptions;

        public AppDbContext(
                    DbContextOptions options,
                    IOptions<CommonDBContextOptions> commonOptions)
            : base(options)
        {
            this.commonOptions = commonOptions.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            commonOptions.OnPreModelCreating?.Invoke(this, modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(commonOptions.ConfigurationAssembly);
        }
    }
}
