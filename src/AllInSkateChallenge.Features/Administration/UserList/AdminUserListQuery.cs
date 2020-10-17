using MediatR;

namespace AllInSkateChallenge.Features.Administration.UserList
{
    public class AdminUserListQuery : IRequest<AdminUserListQueryResponse>
    {
        public int Page { get; set; }
    }
}
