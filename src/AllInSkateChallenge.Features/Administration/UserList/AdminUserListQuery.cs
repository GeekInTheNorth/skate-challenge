namespace AllInSkateChallenge.Features.Administration.UserList;

using MediatR;

public class AdminUserListQuery : IRequest<AdminUserListQueryResponse>
{
    public string SearchText { get; set; }

    public int Page { get; set; }

    public SortOrder SortOrder { get; set; }

    public PaidStatus PaidStatus { get; set; }
}