using System;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Framework.Routing;
using AllInSkateChallenge.Features.Services.Email;

using MediatR;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;

namespace AllInSkateChallenge.Features.Skater.Registration
{
    public class SendRegistrationEmailCommandHandler : IRequestHandler<SendRegistrationEmailCommand>
    {
        private readonly IViewToStringRenderer viewToStringRenderer;

        private readonly IAbsoluteUrlHelper absoluteUrlHelper;

        private readonly IEmailSender emailSender;

        private readonly ILogger<SendRegistrationEmailCommandHandler> logger;

        public SendRegistrationEmailCommandHandler(
            IViewToStringRenderer viewToStringRenderer, 
            IAbsoluteUrlHelper absoluteUrlHelper, 
            IEmailSender emailSender, 
            ILogger<SendRegistrationEmailCommandHandler> logger)
        {
            this.viewToStringRenderer = viewToStringRenderer;
            this.absoluteUrlHelper = absoluteUrlHelper;
            this.emailSender = emailSender;
            this.logger = logger;
        }

        public async Task<Unit> Handle(SendRegistrationEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var emailModel = new RegistrationEmailModel
                {
                    LogoUrl = absoluteUrlHelper.Get("/images/AllInSkateChallengeBanner2.png"),
                    EmailConfirmationUrl = request.EmailConfirmationUrl,
                    SiteUrl = absoluteUrlHelper.Get("/"),
                    LogMilesUrl = absoluteUrlHelper.Get("/skater/skate-log"),
                    SponsorLogoUrl = absoluteUrlHelper.Get("/images/SkateEverywhereLogo.png"),
                };

                var emailBody = await viewToStringRenderer.RenderPartialToStringAsync("~/Views/Email/RegistrationEmail.cshtml", emailModel);

                await emailSender.SendEmailAsync(request.Email, "ALL IN Skate Challenge Progress", emailBody);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to progress updates when saving mileage entries", request);
            }

            return Unit.Value;
        }
    }
}
