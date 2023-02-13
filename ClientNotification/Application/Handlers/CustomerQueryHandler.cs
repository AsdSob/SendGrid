using ClientNotification.Common.Abstractions;
using ClientNotification.Common.Exceptions;
using ClientNotification.Domain.Items;
using ClientNotification.Domain.Requests.Queries;
using ClientNotification.Persistence.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ClientNotification.Application.Handlers
{
    public class CustomerQueryHandler : IQueryHandler<CustomerQuery, CustomerItem>
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerQueryHandler(
                    ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<CustomerItem> Handle(CustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetCustomerByIdAsync(request.CustomerId, cancellationToken);
            if (customer == null)
                throw new NotFoundApiException($"Customer with Id = {request.CustomerId} not found");

            return new CustomerItem()
            {
                Id = customer.Id,
                Name = customer.Name,
                EMail = customer.EMail,
                Amount = customer.Amount,
                CreditNumber = customer.CreditNumber,
                DueDate = customer.DueDate
            };
        }
    }
}
