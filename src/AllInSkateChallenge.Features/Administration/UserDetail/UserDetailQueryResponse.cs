namespace AllInSkateChallenge.Features.Administration.UserDetail;

using System.Collections.Generic;

using AllInSkateChallenge.Features.Administration.UserList;
using AllInSkateChallenge.Features.Data.Entities;

public class UserDetailQueryResponse
{
    public ApplicationUser User { get; set; }

    public List<SkateLogEntry> Activities { get; set; }

    public string UserFilter { get; set; }

    public PaidStatus PaidFilter { get; set; }
    
    public SortOrder FilterOrder { get; set; }

    public int FilterPage { get; set; }
}
