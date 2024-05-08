namespace AllInSkateChallenge.Features.Framework.Header;

using System.Threading.Tasks;

using AllInSkateChallenge.Features.Common;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Gravatar;
using AllInSkateChallenge.Features.Skater.Registration;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

public class HeaderMenuViewComponent(IGravatarResolver gravatarResolver, IMediator mediator, IOptions<ChallengeSettings> settings) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(ApplicationUser user)
    {
        var hasDismissedCookieBanner = Request.Cookies.ContainsKey("cookieWarningDismissed");
        var isRegistrationOver = await mediator.Send(new RegistrationAvailabilityQuery());

        var model = new HeaderMenuViewModel
        {
            UserName = user?.SkaterName,
            UserProfileImage =
                                string.IsNullOrWhiteSpace(user?.ExternalProfileImage)
                                    ? gravatarResolver.GetGravatarUrl(user?.Email)
                                    : user?.ExternalProfileImage,
            IsLoggedIn = user != null,
            ShowCookieBanner = user == null && !hasDismissedCookieBanner,
            IsRegistrationOver = isRegistrationOver,
            IsTeamEvent = settings.Value.ChallengeMode == ChallengeMode.Team
        };

        return View("~/Views/Shared/Components/HeaderMenu/Default.cshtml", model);
    }
}