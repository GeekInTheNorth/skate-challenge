using System;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Framework.Command;
using AllInSkateChallenge.Features.Framework.Routing;
using AllInSkateChallenge.Features.Services.Email;
using AllInSkateChallenge.Features.Skater;

using Microsoft.AspNetCore.Identity.UI.Services;

namespace AllInSkateChallenge.Features.Activities
{
    public class SendProgressUpdateEventHandler : ICommandEventHandler<SaveActivityCommand>
    {
        private readonly ISkaterMileageEntriesRepository repository;

        private readonly ICheckPointRepository checkPointRepository;

        private readonly IViewToStringRenderer viewToStringRenderer;

        private readonly IAbsoluteUrlHelper absoluteUrlHelper;

        private readonly IEmailSender emailSender;

        public SendProgressUpdateEventHandler(
            ISkaterMileageEntriesRepository repository,
            ICheckPointRepository checkPointRepository,
            IViewToStringRenderer viewToStringRenderer,
            IAbsoluteUrlHelper absoluteUrlHelper, 
            IEmailSender emailSender)
        {
            this.repository = repository;
            this.checkPointRepository = checkPointRepository;
            this.viewToStringRenderer = viewToStringRenderer;
            this.absoluteUrlHelper = absoluteUrlHelper;
            this.emailSender = emailSender;
        }

        public async Task HandleAsync(SaveActivityCommand command, CommandResult result)
        {
            if (!result.IsSuccess) return;

            try
            {
                var milesThisSkate = command.Distance;
                switch (command.DistanceUnit)
                {
                    case DistanceUnit.Kilometres:
                        milesThisSkate = milesThisSkate * 0.621371M;
                        break;
                    case DistanceUnit.Metres:
                        milesThisSkate = milesThisSkate * 0.000621371M;
                        break;
                }

                var totalMiles = repository.GetTotalDistance(command.Skater);
                var previousMiles = totalMiles - milesThisSkate;
                var checkPointReached = checkPointRepository.Get().Where(x => x.Distance >= previousMiles && x.Distance <= totalMiles).OrderByDescending(x => x.Distance).FirstOrDefault();

                if (checkPointReached != null && command.Skater.EmailConfirmed)
                {
                    var emailModel = new SkaterProgressEmailViewModel 
                    { 
                        LogoUrl = absoluteUrlHelper.Get("/images/AllInSkateChallengeBanner2.png"),
                        Skater = command.Skater, 
                        CheckPoint = checkPointReached,
                        TotalMiles = totalMiles
                    };
                    var emailBody = await viewToStringRenderer.RenderPartialToStringAsync("~/Views/Email/SkaterProgressEmail.cshtml", emailModel);

                    await emailSender.SendEmailAsync(command.Skater.Email, "ALL IN Skate Challenge Progress", emailBody);
                }
            }
            catch (Exception ex)
            {
                // TODO: Log this
            }
        }
    }
}
