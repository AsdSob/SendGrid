using ClientNotification.Common.Controllers.Abstractions;
using ClientNotification.Common.Models;
using ClientNotification.Domain.DTO;
using ClientNotification.Domain.Requests.Commands;
using ClientNotification.Domain.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClientNotification.Controllers
{
    [Route("api/v1/customers")]
    public class CustomerController : BaseApiController
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<PageCollection<CustomerDTO>> ListAsync([FromQuery] PageFilterDTO pageFilter, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new AllCustomersQuery()
            {
                Page = pageFilter.Page,
                PageSize = pageFilter.Size
            }, cancellationToken: cancellationToken);
            return new PageCollection<CustomerDTO>()
            {
                Page = pageFilter.Page,
                PageSize = pageFilter.Size,
                Total = response.Total,
                Items = response.Items.Select(x => new CustomerDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Amount = x.Amount,
                    Email = x.EMail,
                    CreditNumber = x.CreditNumber,
                    DueDate = x.DueDate
                })
                .ToArray()
            };
        }

        [HttpGet("{id}")]
        public async Task<CustomerDTO> GetAsync(int id, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new CustomerQuery()
            {
                CustomerId = id
            }, cancellationToken: cancellationToken);
            return new CustomerDTO()
            {
                Id = response.Id,
                Name = response.Name,
                Email = response.EMail,
                CreditNumber = response.CreditNumber,
                Amount = response.Amount,
                DueDate = response.DueDate
            };
        }

        [HttpPost]
        public async Task<CustomerDTO> AddAsync(CustomerDTO customer, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new NewCustomerCommand()
            {
                Name = customer.Name,
                Amount = customer.Amount,
                EMail = customer.Email,
                CreditNumber = customer.CreditNumber,
                DueDate = customer.DueDate
            }, cancellationToken: cancellationToken);
            return new CustomerDTO()
            {
                Id = response.Id,
                Name = response.Name,
                Email = response.EMail,
                CreditNumber = response.CreditNumber,
                Amount = response.Amount, 
                DueDate = response.DueDate 
            };
        }
    }
}
