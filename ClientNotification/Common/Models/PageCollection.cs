namespace ClientNotification.Common.Models
{
    public class PageCollection<TClass>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int Total { get; set; }

        public TClass[] Items { get; set; }
    }
}
