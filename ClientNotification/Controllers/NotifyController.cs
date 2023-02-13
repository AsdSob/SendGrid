using ClientNotification.Common.Controllers.Abstractions;
using ClientNotification.Domain.DTO;
using ClientNotification.Domain.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ClientNotification.Controllers
{
    [Route("api/v1/notifies")]
    public class NotifyController : BaseApiController
    {
        private readonly IMediator mediator;

        public NotifyController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<NotifyResponseDTO> SendAsync(NotifyDTO notify, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new SendNotifyCommand()
            {
                CreditNumber = notify.Creditnumber,
                Template = notify.Template
            }, cancellationToken: cancellationToken);

            return new NotifyResponseDTO()
            {
                ResponseBody = response.Body
            };
        } 
    }
}
