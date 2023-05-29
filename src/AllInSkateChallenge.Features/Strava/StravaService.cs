﻿namespace AllInSkateChallenge.Features.Strava;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Strava.Models;

using Microsoft.AspNetCore.Identity;

using Newtonsoft.Json;

public class StravaService : IStravaService
{
    private readonly UserManager<ApplicationUser> userManager;

    public StravaService(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<StravaActivityListResponse> List(ApplicationUser applicationUser)
    {
        var url = "https://www.strava.com/api/v3/athlete/activities?page=1&per_page=100";
        var authToken = await userManager.GetAuthenticationTokenAsync(applicationUser, StravaConstants.ProviderName, StravaConstants.AccessTokenName);
        var responseModel = new StravaActivityListResponse();

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            using (var response = await httpClient.GetAsync(url))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    responseModel.Activities = JsonConvert.DeserializeObject<List<StravaActivity>>(apiResponse);
                }
                else
                {
                    responseModel.Faults = JsonConvert.DeserializeObject<StravaFault>(apiResponse);
                }
            }
        }
        return responseModel;
    }
}