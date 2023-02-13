using System;

namespace ClientNotification.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string EMail { get; set; }

        public string CreditNumber { get; set; }
        
        public double Amount { get; set; }
        
        public DateTime DueDate { get; set; }
    }
}
