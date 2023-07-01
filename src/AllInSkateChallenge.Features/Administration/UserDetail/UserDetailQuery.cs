namespace AllInSkateChallenge.Features.Administration.UserDetail;

using AllInSkateChallenge.Features.Administration.UserList;

using MediatR;

public class UserDetailQuery : IRequest<UserDetailQueryResponse>
{
    public string UserId { get; set; }

    public string UserFilter { get; set; }

    public PaidStatus PaidFilter { get; set; }

    public SortOrder FilterOrder { get; set; }

    public int FilterPage { get; set; }
}