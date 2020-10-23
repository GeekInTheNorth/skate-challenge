namespace AllInSkateChallenge.Features.Administration.UserList
{
    public class AdminUserModel
    {
        public string Id { get; set; }
        public string SkaterName { get; internal set; }
        public string Email { get; internal set; }
        public bool EmailConfirmed { get; internal set; }
        public bool HasPaid { get; internal set; }
        public bool IsAdmin { get; internal set; }
    }
}
