using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data
{
    public class MyDbContext: DbContext
    {


        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<LeaderboardSnapshot> LeaderboardSnapshots { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeaderboardSnapshot>()
                .HasMany(s => s.Entries)
                .WithOne(e => e.Snapshot)
                .HasForeignKey(e => e.SnapshotId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
