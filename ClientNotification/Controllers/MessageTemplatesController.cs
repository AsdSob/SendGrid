using ClientNotification.Common.Controllers.Abstractions;
using ClientNotification.Domain.DTO;
using ClientNotification.Domain.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ClientNotification.Controllers
{
    [Route("api/v1/messagetemplates")]
    public class MessageTemplatesController : BaseApiController
    {
        private readonly IMediator mediator;

        public MessageTemplatesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<MessageTemplateDTO> CreateAsync(MessageTemplateDTO messageTemplate, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new NewMessageTemplateCommand()
            {
                Name = messageTemplate.Name,
                Message = messageTemplate.Message,
            });
            return new MessageTemplateDTO()
            {
                Id = response.Id,
                Message = response.Message,
                Name = response.Name,
            };
        }
    }
}
