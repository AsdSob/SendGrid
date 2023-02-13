using ClientNotification.Common.Abstractions;
using ClientNotification.Domain.Items;

namespace ClientNotification.Domain.Requests.Commands
{
    public class NewMessageTemplateCommand : ICommand<MessageTemplateItem>
    {
        public string Name { get; set; }

        public string Message { get; set; }
    }
}
