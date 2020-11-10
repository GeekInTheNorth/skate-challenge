using System;
using System.Threading;
using System.Threading.Tasks;
using AllInSkateChallenge.Features.Services.Email;
using MediatR;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AllInSkateChallenge.Features.Contact
{
    public class ContactCommandHandler : IRequestHandler<ContactCommand>
    {
        private readonly IEmailSender emailSender;

        private readonly EmailSettings emailSettings;

        private readonly IViewToStringRenderer viewToStringRenderer;

        private readonly ILogger<ContactCommandHandler> logger;

        public ContactCommandHandler(
            IEmailSender emailSender,
            IOptions<EmailSettings> emailSettings,
            IViewToStringRenderer viewToStringRenderer, 
            ILogger<ContactCommandHandler> logger)
        {
            this.emailSender = emailSender;
            this.emailSettings = emailSettings.Value;
            this.viewToStringRenderer = viewToStringRenderer;
            this.logger = logger;
        }

        public async Task<Unit> Handle(ContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var emailBody = await viewToStringRenderer.RenderPartialToStringAsync("~/Views/Email/ContactEmail.cshtml", request);

                await emailSender.SendEmailAsync(emailSettings.SenderEmail, "ALL IN Skate Challenge - Contact Form", emailBody);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to send contact form email", request);
            }

            return Unit.Value;
        }
    }
}
