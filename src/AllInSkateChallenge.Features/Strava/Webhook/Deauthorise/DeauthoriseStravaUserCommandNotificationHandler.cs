namespace AllInSkateChallenge.Features.Strava.Webhook.Deauthorise
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Framework.Routing;
    using AllInSkateChallenge.Features.Services.Email;

    using MediatR.Pipeline;

    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Logging;

    public class DeauthoriseStravaUserCommandNotificationHandler : IRequestPostProcessor<DeauthoriseStravaUserCommand, DeauthoriseStravaUserCommandResponse>
    {
        private readonly IViewToStringRenderer viewToStringRenderer;

        private readonly IAbsoluteUrlHelper absoluteUrlHelper;

        private readonly IEmailSender emailSender;

        private readonly ILogger<DeauthoriseStravaUserCommandNotificationHandler> logger;

        public DeauthoriseStravaUserCommandNotificationHandler(
            IViewToStringRenderer viewToStringRenderer, 
            IAbsoluteUrlHelper absoluteUrlHelper, 
            IEmailSender emailSender, 
            ILogger<DeauthoriseStravaUserCommandNotificationHandler> logger)
        {
            this.viewToStringRenderer = viewToStringRenderer;
            this.absoluteUrlHelper = absoluteUrlHelper;
            this.emailSender = emailSender;
            this.logger = logger;
        }

        public async Task Process(DeauthoriseStravaUserCommand request, DeauthoriseStravaUserCommandResponse response, CancellationToken cancellationToken)
        {
            if (!response.WasSuccessful) return;

            try
            {
                response.LogoUrl = absoluteUrlHelper.Get("/rggeventone/images/banner-desktop.png");
                var emailBody = await viewToStringRenderer.RenderPartialToStringAsync("~/Views/Email/DeAuthoriseEmail.cshtml", response);

                await emailSender.SendEmailAsync(response.UserDetails.Email, "Roller Girl Gang Virtual Skate Marathon", emailBody);
            }
            catch(Exception exception)
            {
                logger.LogError(exception, "Failed to send email notification for a deauthorise event.", request);
            }
        }
    }
}
