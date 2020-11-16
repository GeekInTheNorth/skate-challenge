using System.Collections.Generic;

namespace AllInSkateChallenge.Features.Administration.UserList
{
    public class AdminUserListQueryResponse
    {
        public List<AdminUserModel> Users { get; set; }

        public string SearchText { get; set; }

        public int TotalUsers { get; set; }

        public int CurrentPage { get; set; }

        public int MaxPages { get; set; }
    }
}
