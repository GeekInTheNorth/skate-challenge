using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Administration
{
    public class UserAdministrationViewModel
    {
        public List<ApplicationUser> Users { get; set; }

        public int TotalUsers { get; set; }

        public int CurrentPage { get; set; }

        public int MaxPages { get; set; }
    }
}
