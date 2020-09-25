using Microsoft.AspNetCore.Identity;

namespace AllInSkateChallenge.Features.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string SkaterName { get; set; }
    }
}
