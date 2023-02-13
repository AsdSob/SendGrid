using System;

namespace ClientNotification.Domain.Items
{
    public class CustomerItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string EMail { get; set; }

        public string CreditNumber { get; set; }

        public double Amount { get; set; }

        public DateTime DueDate { get; set; }
    }
}
