namespace ClientNotification.Application.Configs
{
    public class SendGridOptions
    {
        public string APIKey { get; set; }

        public string FromEMail { get; set; }

        public string FromUser { get; set; }
    }
}
