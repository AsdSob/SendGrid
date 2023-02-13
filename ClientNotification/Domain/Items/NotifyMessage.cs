namespace ClientNotification.Domain.Items
{
    public class NotifyMessage
    {
        public string ToEMail { get; set; }

        public string ToUser { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
