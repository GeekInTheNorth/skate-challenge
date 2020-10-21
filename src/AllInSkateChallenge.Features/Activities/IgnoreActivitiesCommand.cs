using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

namespace AllInSkateChallenge.Features.Activities
{
    public class IgnoreActivitiesCommand : IRequest<IgnoreActivitiesCommandResponse>
    {
        public ApplicationUser Skater { get; set; }

        public List<string> StravaActivityIds { get; set; }
    }
}
