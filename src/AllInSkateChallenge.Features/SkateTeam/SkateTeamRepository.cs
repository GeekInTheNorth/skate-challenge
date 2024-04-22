using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Kontent;
using AllInSkateChallenge.Features.Extensions;
using Kontent.Ai.Delivery.Abstractions;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.SkateTeam;

public class SkateTeamRepository(IDeliveryClient deliveryClient, UserManager<ApplicationUser> userManager) : ISkateTeamRepository
{
    private List<SkateTeamModel> _skateTeams;

    public async Task<List<SkateTeamModel>> GetAsync()
    {
        if (_skateTeams is { Count: > 0 })
        {
            return _skateTeams;
        }

        var response = await deliveryClient.GetItemsAsync<SkateTeamData>();

        var teamCounts = await userManager.Users.GroupBy(x => x.Target).Select(x => new { Team = x.Key, Skaters = x.Count() }).ToListAsync();

        return _skateTeams = response.Items
            .OrderBy(x => x.UrlSlug)
            .Select((x, index) => new SkateTeamModel
            {
                Id = index + 1,
                Name = x.TeamName,
                Logo = x.TeamLogo.GetSingleUrl(),
                PlainDescription = x.TeamDescriptionPlain,
                RichDescription = x.TeamDescriptionRich,
                TeamMembers = teamCounts.FirstOrDefault(x => x.Team == index)?.Skaters ?? 0
            }).ToList();
    }
}