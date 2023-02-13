using System.Threading.Tasks;
using System.Threading;

namespace ClientNotification.Common.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
