using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Administration.UserDetail
{
    public class UserDetailQueryResponse
    {
        public ApplicationUser User { get; set; }

        public List<SkateLogEntry> Activities { get; set; }

        public string UserFilter { get; set; }
    }
}
