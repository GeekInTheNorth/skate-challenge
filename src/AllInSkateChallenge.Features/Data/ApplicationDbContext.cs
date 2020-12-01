using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AllInSkateChallenge.Features.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<SkateLogEntry> SkateLogEntries { get; set; }

        public DbSet<StravaIntegrationLog> StravaIntegrationLogs { get; set; }

        public DbSet<StravaEvent> StravaEvents { get; set; }

        public DbSet<EventStatistic> EventStatistics { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var skateTargetConverter = new EnumToNumberConverter<SkateTarget, int>();
            builder.Entity<ApplicationUser>().Property(x => x.DateRegistered).HasDefaultValueSql("getdate()");
            builder.Entity<ApplicationUser>().Property(x => x.Target).HasConversion(skateTargetConverter).HasDefaultValue(SkateTarget.LiverpoolCanningDock);

            builder.Entity<SkateLogEntry>().HasOne(x => x.ApplicationUser).WithMany(x => x.SkateLogEntries).HasForeignKey(x => x.ApplicationUserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<SkateLogEntry>().HasIndex(x => new { x.ApplicationUserId, x.Logged });

            builder.Entity<StravaEvent>().HasOne(x => x.ApplicationUser).WithMany(x => x.StravaEvents).HasForeignKey(x => x.ApplicationUserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StravaEvent>().HasIndex(x => new { x.ApplicationUserId, x.StravaActivityId });
        }
    }
}
