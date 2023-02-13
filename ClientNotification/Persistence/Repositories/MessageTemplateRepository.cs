using ClientNotification.Common.Persistence;
using ClientNotification.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace ClientNotification.Persistence.Repositories
{
    public class MessageTemplateRepository : IMessageTemplateRepository
    {
        private readonly AppDbContext dbContext;

        public MessageTemplateRepository(AppDbContext dbContext) =>
            this.dbContext = dbContext;

        public Task<MessageTemplate> GetMessageTemplateByName(string name, CancellationToken cancellationToken) =>
            dbContext.Set<MessageTemplate>()
                     .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        public async Task AddMessageTemplateAsync(MessageTemplate messageTemplate, CancellationToken cancellationToken) =>
            await dbContext.Set<MessageTemplate>()
                           .AddAsync(messageTemplate, cancellationToken);
    }
}
