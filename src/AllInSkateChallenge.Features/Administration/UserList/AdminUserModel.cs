using System;

namespace AllInSkateChallenge.Features.Administration.UserList
{
    public class AdminUserModel
    {
        public string Id { get; set; }
        public string SkaterName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool HasPaid { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsStravaAccount { get; set; }
        public DateTime DateRegistered { get; set; }
        public int NoOfActivities { get; set; }
    }
}
