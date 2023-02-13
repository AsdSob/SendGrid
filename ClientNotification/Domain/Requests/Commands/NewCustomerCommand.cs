using ClientNotification.Common.Abstractions;
using ClientNotification.Domain.Items;
using System;

namespace ClientNotification.Domain.Requests.Commands
{
    public class NewCustomerCommand : ICommand<CustomerItem>
    {
        public string Name { get; set; }

        public string CreditNumber { get; set; }

        public string EMail { get; set; }

        public double Amount { get; set; }

        public DateTime DueDate { get; set; }
    }
}
