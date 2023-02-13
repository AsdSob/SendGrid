using ClientNotification.Common.Abstractions;
using ClientNotification.Domain.Entities;
using ClientNotification.Domain.Items;
using ClientNotification.Domain.Requests.Queries;
using ClientNotification.Persistence.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClientNotification.Application.Handlers
{
    public class AllCustomersQueryHandler : IQueryHandler<AllCustomersQuery, CustomerItems>
    {
        private readonly ICustomerRepository customerRepository;

        public AllCustomersQueryHandler(
                        ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<CustomerItems> Handle(AllCustomersQuery request, CancellationToken cancellationToken)
        {
            var total = await customerRepository.GetCustomerCountAsync(cancellationToken);
            var customers = (total == 0) ? 
                                Array.Empty<Customer>() :
                                await customerRepository.GetCustomersAsync(request.Page, request.PageSize, cancellationToken);

            return new CustomerItems()
            {
                Total = total,
                Items = customers.Select(x => new CustomerItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    EMail = x.EMail,
                    CreditNumber = x.CreditNumber,
                    Amount = x.Amount,
                    DueDate = x.DueDate
                })
                .ToArray()
            }; 
        }
    }
}
