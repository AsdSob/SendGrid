using ClientNotification.Common.Abstractions;
using ClientNotification.Domain.Items;

namespace ClientNotification.Domain.Requests.Queries
{
    public class AllCustomersQuery : IQuery<CustomerItems>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
