using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

namespace AllInSkateChallenge.Features.Framework.Models
{
    public class UserStateQuery : IRequest<UserStateQueryResponse>
    {
        public ApplicationUser User { get; set; }
    }
}
