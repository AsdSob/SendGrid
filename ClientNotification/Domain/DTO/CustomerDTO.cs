using System;
using System.ComponentModel.DataAnnotations;

namespace ClientNotification.Domain.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string CreditNumber { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}
