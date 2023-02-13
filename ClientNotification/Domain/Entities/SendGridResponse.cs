using System;

namespace ClientNotification.Domain.Entities
{
    public class SendGridResponse
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int TemplateId { get; set; }

        public DateTime Created { get; set; }

        public string Response { get; set; }

        public Customer Customer { get; set; }

        public MessageTemplate Template { get; set; }
    }
}
