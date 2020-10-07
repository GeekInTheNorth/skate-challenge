using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

namespace AllInSkateChallenge.Features.Services.Strava
{

    public class StravaService : IStravaService
    {
        private readonly StravaSettings stravaSettings;

        private readonly UserManager<ApplicationUser> userManager;

        public StravaService(
            IOptions<StravaSettings> stravaSettings, 
            UserManager<ApplicationUser> userManager)
        {
            this.stravaSettings = stravaSettings.Value;
            this.userManager = userManager;
        }

        public async Task<List<StravaActivity>> List(ApplicationUser applicationUser)
        {
            var url = "https://www.strava.com/api/v3/athlete/activities?page=1&per_page=30";

            var authToken = await userManager.GetAuthenticationTokenAsync(applicationUser, StravaConstants.ProviderName, StravaConstants.AccessTokenName);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                using (var response = await httpClient.GetAsync(url))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<StravaActivity>>(apiResponse);
                }
            }
        }
    }
}
