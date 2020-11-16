using MediatR;

namespace AllInSkateChallenge.Features.Administration.UserList
{
    public class AdminUserListQuery : IRequest<AdminUserListQueryResponse>
    {
        public string SearchText { get; set; }

        public int Page { get; set; }
    }
}
