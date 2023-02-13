using ClientNotification.Common.Persistence.Abstractions;
using ClientNotification.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ClientNotification.Persistence.Repositories
{
    public interface IMessageTemplateRepository : IRepository
    {
        Task AddMessageTemplateAsync(MessageTemplate messageTemplate, CancellationToken cancellationToken);

        Task<MessageTemplate> GetMessageTemplateByName(string name, CancellationToken cancellationToken);
    }
}
