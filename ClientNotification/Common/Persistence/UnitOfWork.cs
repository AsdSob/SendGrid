using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace ClientNotification.Common.Persistence
{
    public sealed class UnitOfWork<TDBContext> : IUnitOfWork where TDBContext : DbContext
    {
        private readonly DbContext dbContext;

        public UnitOfWork(TDBContext dbContext) =>
            this.dbContext = dbContext;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            dbContext.SaveChangesAsync(cancellationToken);
    }
}
