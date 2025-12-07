using Microsoft.EntityFrameworkCore;
using Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanupService.Data
{
    public class MyDbContext: DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<LeaderboardSnapshot> LeaderboardSnapshots { get; set; }

    }
}
