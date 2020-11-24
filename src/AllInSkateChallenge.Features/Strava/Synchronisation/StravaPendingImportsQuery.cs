using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

namespace AllInSkateChallenge.Features.Strava
{
    public class StravaPendingImportsQuery : IRequest<StravaImportPendingImportsResponse>
    {
        public ApplicationUser applicationUser { get; set; }
    }
}
