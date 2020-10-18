using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{
    public class SkaterLogQuery : IRequest<SkaterLogQueryResponse>
    {
        public ApplicationUser Skater { get; set; }
    }
}
