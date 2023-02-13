using ClientNotification.Common.Abstractions;
using ClientNotification.Domain.Items;
using MediatR;

namespace ClientNotification.Domain.Requests.Commands
{
    public class SendNotifyCommand : ICommand<NotifyResponse>
    {
        public string CreditNumber { get; set; }

        public string Template { get; set; }
    }
}
