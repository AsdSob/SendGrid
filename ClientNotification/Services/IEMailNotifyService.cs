using ClientNotification.Domain.Items;
using System.Threading;
using System.Threading.Tasks;

namespace ClientNotification.Services
{
    public interface IEMailNotifyService
    {
        Task<string> SendMessage(NotifyMessage notifyMessage, CancellationToken cancellationToken);
    }
}
