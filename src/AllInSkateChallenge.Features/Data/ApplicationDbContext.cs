using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<MileageEntry> MileageEntries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MileageEntry>().HasIndex(x => new { x.UserId, x.MileageEntryId });
        }
    }
}
