using ClientNotification.Common.Abstractions;
using ClientNotification.Domain.Items;

namespace ClientNotification.Domain.Requests.Queries
{
    public class CustomerQuery : IQuery<CustomerItem>
    {
        public int CustomerId { get; set; }
    }
}
