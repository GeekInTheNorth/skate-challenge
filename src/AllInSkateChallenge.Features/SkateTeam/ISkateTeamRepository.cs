using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllInSkateChallenge.Features.SkateTeam;

public interface ISkateTeamRepository
{
    Task<List<SkateTeamModel>> GetAsync();
}
