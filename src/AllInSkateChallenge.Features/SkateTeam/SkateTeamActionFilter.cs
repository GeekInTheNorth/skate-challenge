using System;

using AllInSkateChallenge.Features.Common;
using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace AllInSkateChallenge.Features.SkateTeam;

public sealed class SkateTeamActionFilter(IOptions<ChallengeSettings> options, UserManager<ApplicationUser> userManager) : IActionFilter
{
    private const string CookieName = "SkateTeam";

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Do Nothing
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        try
        {
            if (ShouldCheckTeam(context.HttpContext, options.Value))
            {
                var user = userManager.GetUserAsync(context.HttpContext.User).GetAwaiter().GetResult();
                if (user is { Team: >0 })
                {
                    context.HttpContext.Response.Cookies.Append(CookieName, user.Team.ToString("F0"));
                }
                else
                {
                    context.HttpContext.Response.Redirect("/team/options");
                }
            }
        }
        catch (Exception)
        {
            // Discard
        }
    }

    private static bool ShouldCheckTeam(HttpContext context, ChallengeSettings settings)
    {
        if (settings.ChallengeMode != ChallengeMode.Team)
        {
            return false;
        }

        if (!context.User.Identity.IsAuthenticated)
        {
            return false;
        }

        if (context.Request.Cookies.ContainsKey(CookieName))
        {
            return false;
        }

        return true;
    }
}