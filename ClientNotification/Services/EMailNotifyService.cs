using ClientNotification.Application.Configs;
using ClientNotification.Domain.Items;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace ClientNotification.Services
{
    public class EMailNotifyService : IEMailNotifyService
    {
        private readonly SendGridOptions sendGridOptions;

        public EMailNotifyService(IOptions<SendGridOptions> sendGridOptions)
        {
            this.sendGridOptions = sendGridOptions.Value;
        }

        public async Task<string> SendMessage(NotifyMessage notifyMessage, CancellationToken cancellationToken)
        {
            var client = new SendGridClient(sendGridOptions.APIKey);
            var from = new EmailAddress(sendGridOptions.FromEMail, sendGridOptions.FromUser);
            var to = new EmailAddress(notifyMessage.ToEMail, notifyMessage.ToUser);
            var msg = MailHelper.CreateSingleEmail(from, to, notifyMessage.Subject, notifyMessage.Body, notifyMessage.Body);
            var response = await client.SendEmailAsync(msg, cancellationToken);
            return response.Body != null ? await response.Body.ReadAsStringAsync(cancellationToken) : response.StatusCode.ToString();
        }
    }
}
