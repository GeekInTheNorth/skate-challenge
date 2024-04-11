namespace AllInSkateChallenge.Features.Activities;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Common;
using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Kontent;
using AllInSkateChallenge.Features.Framework.Routing;
using AllInSkateChallenge.Features.Services.Email;
using AllInSkateChallenge.Features.Skater;

using MediatR.Pipeline;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;

public class SendProgressUpdateEventHandler : IRequestPostProcessor<SaveActivityCommand, SaveActivityCommandResult>
{
    private readonly ApplicationDbContext context;

    private readonly ICheckPointRepository checkPointRepository;

    private readonly IViewToStringRenderer viewToStringRenderer;

    private readonly IAbsoluteUrlHelper absoluteUrlHelper;

    private readonly IEmailSender emailSender;

    private readonly ILogger<SaveActivityCommandHandler> logger;

    public SendProgressUpdateEventHandler(
        ApplicationDbContext context,
        ICheckPointRepository checkPointRepository,
        IViewToStringRenderer viewToStringRenderer,
        IAbsoluteUrlHelper absoluteUrlHelper,
        IEmailSender emailSender, 
        ILogger<SaveActivityCommandHandler> logger)
    {
        this.context = context;
        this.checkPointRepository = checkPointRepository;
        this.viewToStringRenderer = viewToStringRenderer;
        this.absoluteUrlHelper = absoluteUrlHelper;
        this.emailSender = emailSender;
        this.logger = logger;
    }

    public async Task Process(SaveActivityCommand request, SaveActivityCommandResult response, CancellationToken cancellationToken)
    {
        if (!response.WasSuccessful || !request.Skater.AcceptProgressNotifications || !request.Skater.EmailConfirmed) return;

        try
        {
            var kilometresThisSkate = GetDistanceInKilometres(request.DistanceUnit, request.Distance);
            var totalKilometres = context.SkateLogEntries.Where(x => x.ApplicationUserId.Equals(request.Skater.Id)).ToList().Sum(x => x.DistanceInKilometres);
            var previousKilometres = totalKilometres - kilometresThisSkate;
            var checkPoints = checkPointRepository.Get().Where(x => x.SkateTarget <= request.Skater.Target).ToList();
            var targetCheckPoint = checkPoints.Last();
            var checkPointsReached = checkPoints.Where(x => x.DistanceInKilometers >= previousKilometres && x.DistanceInKilometers <= totalKilometres).OrderByDescending(x => x.DistanceInKilometers).ToList();

            foreach(var checkPointReached in checkPointsReached)
            {
                var isFinalCheckpoint = checkPointReached.SkateTarget.Equals(request.Skater.Target);
                var emailModel = new SkaterProgressEmailViewModel
                {
                    LogoUrl = absoluteUrlHelper.Get("/rggeventone/images/banner-mobile.png"),
                    Skater = request.Skater,
                    CheckPoint = checkPointReached,
                    TotalKilometres = totalKilometres,
                    NextCheckPoint = checkPoints.Where(x => x.DistanceInKilometers > totalKilometres).OrderBy(x => x.DistanceInKilometers).FirstOrDefault(),
                    TargetCheckPoint = targetCheckPoint
                };

                var emailTemplate = isFinalCheckpoint ? "~/Views/Email/ChallengeCompleteEmail.cshtml" : "~/Views/Email/SkaterProgressEmail.cshtml";
                var emailBody = await viewToStringRenderer.RenderPartialToStringAsync(emailTemplate, emailModel);

                await emailSender.SendEmailAsync(request.Skater.Email, "Roller Girl Gang Virtual Skate Marathon - Your Progress", emailBody);
            }
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to progress updates when saving mileage entries");
        }
    }

    private static decimal GetDistanceInKilometres(DistanceUnit distanceUnit, decimal distance)
    {
        return distanceUnit switch
        {
            DistanceUnit.Metres => Conversion.MetresToKilometres(distance),
            DistanceUnit.Miles => Conversion.MilesToKilometres(distance),
            DistanceUnit.Feet => Conversion.FeetToKilometres(distance),
            _ => distance,
        };
    }
}