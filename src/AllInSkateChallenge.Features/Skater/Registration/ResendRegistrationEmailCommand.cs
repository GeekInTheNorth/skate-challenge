using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Framework.Routing;
using AllInSkateChallenge.Features.Services.Email;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace AllInSkateChallenge.Features.Skater.Registration
{
    public class ResendRegistrationEmailCommand : IRequest
    {
        public string UserId { get; set; }
    }

    public class ResendRegistrationEmailCommandHandler : IRequestHandler<ResendRegistrationEmailCommand>
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IViewToStringRenderer viewToStringRenderer;

        private readonly IAbsoluteUrlHelper absoluteUrlHelper;

        private readonly IEmailSender emailSender;

        private readonly ICheckPointRepository checkPointRepository;

        private readonly ILogger<ResendRegistrationEmailCommandHandler> logger;

        public ResendRegistrationEmailCommandHandler(
            UserManager<ApplicationUser> userManager,
            IViewToStringRenderer viewToStringRenderer,
            IAbsoluteUrlHelper absoluteUrlHelper, 
            IEmailSender emailSender, 
            ICheckPointRepository checkPointRepository, 
            ILogger<ResendRegistrationEmailCommandHandler> logger)
        {
            this.userManager = userManager;
            this.absoluteUrlHelper = absoluteUrlHelper;
            this.viewToStringRenderer = viewToStringRenderer;
            this.emailSender = emailSender;
            this.checkPointRepository = checkPointRepository;
            this.logger = logger;
        }

        public async Task Handle(ResendRegistrationEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new EntityNotFoundException(typeof(ApplicationUser), Guid.Parse(request.UserId));
                }

                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = $"/Identity/Account/ConfirmEmail/?userId={request.UserId}&code={code}&returnUrl=%2F";
                callbackUrl = absoluteUrlHelper.Get(callbackUrl);

                var startPoint = checkPointRepository.Get().OrderBy(x => x.DistanceInKilometers).FirstOrDefault();

                var emailModel = new RegistrationEmailModel
                {
                    LogoUrl = absoluteUrlHelper.Get("/rggeventone/images/banner-mobile.png"),
                    EmailConfirmationUrl = callbackUrl,
                    SiteUrl = absoluteUrlHelper.Get("/"),
                    LogMilesUrl = absoluteUrlHelper.Get("/skater/skate-log"),
                    StartingPostCard = absoluteUrlHelper.Get(startPoint?.Image),
                    FromSkateEverywhere = false
                };

                var emailBody = await viewToStringRenderer.RenderPartialToStringAsync("~/Views/Email/RegistrationEmail.cshtml", emailModel);

                await emailSender.SendEmailAsync(user.Email, "Roller Girl Gang Virtual Skate Marathon Registration", emailBody);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to progress updates when saving mileage entries", request);
            }
        }
    }
}
