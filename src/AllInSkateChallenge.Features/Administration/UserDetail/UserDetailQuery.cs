using MediatR;

namespace AllInSkateChallenge.Features.Administration.UserDetail
{
    public class UserDetailQuery : IRequest<UserDetailQueryResponse>
    {
        public string UserId { get; set; }
    }
}
