using ClientNotification.Common.Persistence;
using ClientNotification.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace ClientNotification.Persistence.Repositories
{
    public class SendGridResponseRepository : ISendGridResponseRepository
    {
        private readonly AppDbContext dbContext;

        public SendGridResponseRepository(AppDbContext dbContext) =>
            this.dbContext = dbContext;

        public async Task AddSendGridResponseAsync(SendGridResponse sendGridEntity, CancellationToken cancellationToken) =>
            await dbContext.Set<SendGridResponse>()
                           .AddAsync(sendGridEntity, cancellationToken);
    }
}
