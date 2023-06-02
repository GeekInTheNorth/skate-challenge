namespace AllInSkateChallenge.Features.Strava.User;

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Identity;

public static class UserManagerExtensions
{
    public static async Task<StavaDetails> GetStravaDetails(this UserManager<ApplicationUser> userManager, ClaimsPrincipal claimsPrincipal)
    {
        var stravaDetails = new StavaDetails
        {
            User = await userManager.GetUserAsync(claimsPrincipal)
        };

        var isStravaAuthenticated = claimsPrincipal.HasClaim(x => x.Type == ClaimTypes.AuthenticationMethod && x.Value == StravaConstants.ProviderName);
        if (isStravaAuthenticated)
        {
            var providers = await userManager.GetLoginsAsync(stravaDetails.User);
            var stravaId = providers?.FirstOrDefault(x => x.LoginProvider == StravaConstants.ProviderName)?.ProviderKey;

            stravaDetails.StravaId = stravaId;
            stravaDetails.IsStravaAuthenticated = !string.IsNullOrWhiteSpace(stravaId);
        }

        return stravaDetails;
    }
}