namespace AllInSkateChallenge.Features.Strava;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Strava.Models;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class StravaImportPendingImportsQueryHandler : IRequestHandler<StravaPendingImportsQuery, StravaImportPendingImportsResponse>
{
    private readonly ApplicationDbContext context;

    private readonly UserManager<ApplicationUser> userManager;

    private readonly ILogger<StravaImportPendingImportsQueryHandler> logger;

    public StravaImportPendingImportsQueryHandler(
        ApplicationDbContext context, 
        UserManager<ApplicationUser> userManager, 
        ILogger<StravaImportPendingImportsQueryHandler> logger)
    {
        this.context = context;
        this.userManager = userManager;
        this.logger = logger;
    }

    public async Task<StravaImportPendingImportsResponse> Handle(StravaPendingImportsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await context.StravaEvents.Where(x => x.ApplicationUserId.Equals(request.applicationUser.Id) && !x.Imported).ToListAsync();
        var response = new StravaImportPendingImportsResponse { Activities = new List<StravaActivity>() };
        var authToken = await userManager.GetAuthenticationTokenAsync(request.applicationUser, StravaConstants.ProviderName, StravaConstants.AccessTokenName);

        foreach (var notification in notifications)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                var url = $"https://www.strava.com/api/v3/activities/{notification.StravaActivityId}?include_all_efforts=false";

                using (var stravaResponse = await httpClient.GetAsync(url))
                {
                    var apiResponse = await stravaResponse.Content.ReadAsStringAsync();
                    await SaveEventDataToLog(apiResponse);

                    if (stravaResponse.IsSuccessStatusCode)
                    {
                        response.Activities.Add(JsonConvert.DeserializeObject<StravaActivity>(apiResponse));
                    }
                }
            }
        }

        return response;
    }

    private async Task SaveEventDataToLog(string apiResponse)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(apiResponse))
            {
                return;
            }

            var log = new StravaIntegrationLog
            {
                Recieved = DateTime.Now,
                Body = JValue.Parse(apiResponse).ToString(Formatting.Indented),
            };

            context.StravaIntegrationLogs.Add(log);
            await context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to log strava activity data");
        }
    }
}