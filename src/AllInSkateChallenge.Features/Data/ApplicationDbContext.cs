using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<MileageEntry> MileageEntries { get; set; }

        public DbSet<SkateLogEntry> SkateLogEntries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MileageEntry>().HasIndex(x => new { x.UserId, x.MileageEntryId });
            builder.Entity<SkateLogEntry>().HasOne(x => x.ApplicationUser).WithMany(x => x.SkateLogEntries).HasForeignKey(x => x.ApplicationUserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<SkateLogEntry>().HasIndex(x => new { x.ApplicationUserId, x.Logged });
        }
    }
}
