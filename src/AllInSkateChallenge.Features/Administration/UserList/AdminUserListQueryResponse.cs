namespace AllInSkateChallenge.Features.Administration.UserList;

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

public class AdminUserListQueryResponse
{
    public List<AdminUserModel> Users { get; set; }

    public int TotalUsers { get; set; }

    public int CurrentPage { get; set; }

    public int MaxPages { get; set; }

    public List<SelectListItem> SortOrders { get; set; }

    public List<SelectListItem> PaidStates { get; set; }

    public string UserFilter { get; set; }

    public PaidStatus PaidFilter { get; set; }

    public SortOrder FilterOrder { get; set; }
}