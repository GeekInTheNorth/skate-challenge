namespace AllInSkateChallenge.Features.Administration.UserList;

using MediatR;

public class AdminUserListQuery : IRequest<AdminUserListQueryResponse>
{
    public string UserFilter { get; set; }

    public PaidStatus PaidFilter { get; set; }

    public SortOrder FilterOrder { get; set; }

    public int FilterPage { get; set; }
}