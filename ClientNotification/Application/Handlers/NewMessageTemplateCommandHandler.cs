using ClientNotification.Common.Abstractions;
using ClientNotification.Common.Persistence;
using ClientNotification.Domain.Entities;
using ClientNotification.Domain.Items;
using ClientNotification.Domain.Requests.Commands;
using ClientNotification.Persistence.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ClientNotification.Application.Handlers
{
    public class NewMessageTemplateCommandHandler : ICommandHandler<NewMessageTemplateCommand, MessageTemplateItem>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMessageTemplateRepository templateRepository;

        public NewMessageTemplateCommandHandler(
                     IUnitOfWork unitOfWork, 
                     IMessageTemplateRepository templateRepository)
        {
            this.unitOfWork = unitOfWork;
            this.templateRepository = templateRepository;
        }

        public async Task<MessageTemplateItem> Handle(NewMessageTemplateCommand request, CancellationToken cancellationToken)
        {
            var messageTemplate = new MessageTemplate()
            {
                Message = request.Message,
                Name = request.Name,
            };

            await templateRepository.AddMessageTemplateAsync(messageTemplate, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new MessageTemplateItem()
            {
                Id = messageTemplate.Id,
                Name = messageTemplate.Name,
                Message = messageTemplate.Message
            };
        }
    }
}
