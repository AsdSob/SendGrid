using System.ComponentModel.DataAnnotations;

namespace ClientNotification.Domain.DTO
{
    public class MessageTemplateDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
