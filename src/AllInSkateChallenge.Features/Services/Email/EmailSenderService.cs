using System;
using System.Threading.Tasks;

using Azure.Communication.Email;

using MailKit.Net.Smtp;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

using MimeKit;

namespace AllInSkateChallenge.Features.Services.Email
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
            if (string.IsNullOrWhiteSpace(_emailSettings.CommResourceConnectionString))
            {
                await SendStandardEmailAsync(email, subject, htmlMessage);
            }
            else
            {
                await SendCommResourceEmailAsync(email, subject, htmlMessage);
            }
        }

        private async Task SendStandardEmailAsync(string email, string subject, string htmlMessage)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Roller Girl Gang Skate Marathon", _emailSettings.SenderEmail));
            mimeMessage.To.Add(new MailboxAddress(email, email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, true);
                await smtpClient.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
                await smtpClient.SendAsync(mimeMessage);
                await smtpClient.DisconnectAsync(true);
            }
        }

        private async Task SendCommResourceEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var emailClient = new EmailClient(_emailSettings.CommResourceConnectionString);
                var result = await emailClient.SendAsync(
                    Azure.WaitUntil.Started,
                    _emailSettings.SenderEmail,
                    email,
                    subject,
                    htmlMessage);
            }
            catch (Exception)
            {
                // Log the exception
            }
        }
    }
}
