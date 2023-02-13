using ClientNotification.Common.Persistence;
using ClientNotification.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClientNotification.Persistence.Repositories
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext dbContext;

        public CustomerRepository(AppDbContext dbContext) =>
            this.dbContext = dbContext;

        public Task<Customer> GetCustomerByIdAsync(int customerId, CancellationToken cancellationToken) =>
            dbContext.Set<Customer>()
                     .FirstOrDefaultAsync(x => x.Id == customerId, cancellationToken);

        public Task<Customer> GetCustomerByCreditNumberAsync(string creditNumber, CancellationToken cancellationToken) =>
            dbContext.Set<Customer>()
                     .FirstOrDefaultAsync(x => x.CreditNumber == creditNumber, cancellationToken);

        public async Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken) =>
            await dbContext.Set<Customer>()
                           .AddAsync(customer, cancellationToken);

        public Task<Customer[]> GetCustomersAsync(int page, int pageSize, CancellationToken cancellationToken) =>
            dbContext.Set<Customer>()
                     .OrderBy(x => x.Id)
                     .Skip(page * pageSize)
                     .Take(pageSize)
                     .ToArrayAsync(cancellationToken);

        public Task<int> GetCustomerCountAsync(CancellationToken cancellationToken) =>
            dbContext.Set<Customer>()
                     .CountAsync(cancellationToken);
    }
}
