using ClientNotification.Common.Persistence.Abstractions;
using ClientNotification.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ClientNotification.Persistence.Repositories
{
    public interface ISendGridResponseRepository : IRepository
    {
        Task AddSendGridResponseAsync(SendGridResponse sendGridEntity, CancellationToken cancellationToken);
    }
}
