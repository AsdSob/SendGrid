using ClientNotification.Common.Persistence.Abstractions;
using ClientNotification.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ClientNotification.Persistence.Repositories
{
    public interface ICustomerRepository : IRepository
    {
        Task<Customer> GetCustomerByIdAsync(int customerId, CancellationToken cancellationToken);

        Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken);

        Task<Customer[]> GetCustomersAsync(int page, int pageSize, CancellationToken cancellationToken);
        
        Task<int> GetCustomerCountAsync(CancellationToken cancellationToken);

        Task<Customer> GetCustomerByCreditNumberAsync(string creditNumber, CancellationToken cancellationToken);
    }
}
