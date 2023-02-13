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
    public class NewCustomerCommandHandler : ICommandHandler<NewCustomerCommand, CustomerItem>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICustomerRepository customerRepository;

        public NewCustomerCommandHandler(
                    IUnitOfWork unitOfWork,
                    ICustomerRepository customerRepository)
        {
            this.unitOfWork = unitOfWork;
            this.customerRepository = customerRepository;
        }

        public async Task<CustomerItem> Handle(NewCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer()
            {
                DueDate = request.DueDate,
                Name = request.Name,
                EMail = request.EMail,
                CreditNumber = request.CreditNumber,
                Amount = request.Amount,
            };

            await customerRepository.AddCustomerAsync(customer, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

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
