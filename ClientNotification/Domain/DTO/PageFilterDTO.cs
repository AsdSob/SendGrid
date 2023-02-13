using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ClientNotification.Domain.DTO
{
    public class PageFilterDTO
    {
        [FromQuery(Name = "page")]
        public int Page { get; set; } = 0;


        [FromQuery(Name = "size")]
        public int Size { get; set; } = 20;
    }
}
