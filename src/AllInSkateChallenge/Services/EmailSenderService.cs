using System.Threading.Tasks;

using MailKit.Net.Smtp;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

using MimeKit;

namespace AllInSkateChallenge.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSenderService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("All In Skate Challenge", _emailSettings.SenderEmail));
            mimeMessage.To.Add(new MailboxAddress(email, email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = htmlMessage };

            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, true);
                await smtpClient.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
                await smtpClient.SendAsync(mimeMessage);
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}
