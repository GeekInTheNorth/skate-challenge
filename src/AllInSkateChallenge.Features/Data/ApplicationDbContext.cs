using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<SkateLogEntry> SkateLogEntries { get; set; }

        public DbSet<StravaIntegrationLog> StravaIntegrationLogs { get; set; }

        public DbSet<StravaEvent> StravaEvents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SkateLogEntry>().HasOne(x => x.ApplicationUser).WithMany(x => x.SkateLogEntries).HasForeignKey(x => x.ApplicationUserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<SkateLogEntry>().HasIndex(x => new { x.ApplicationUserId, x.Logged });

            builder.Entity<StravaEvent>().HasOne(x => x.ApplicationUser).WithMany(x => x.StravaEvents).HasForeignKey(x => x.ApplicationUserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StravaEvent>().HasIndex(x => new { x.ApplicationUserId, x.StravaActivityId });
        }
    }
}
