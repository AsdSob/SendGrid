using System.ComponentModel.DataAnnotations;

namespace ClientNotification.Domain.DTO
{
    public class NotifyDTO
    {
        [Required]
        public string Creditnumber { get; set; }

        [Required]
        public string Template { get; set; }
    }
}
