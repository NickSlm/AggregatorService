using Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggregatorService.Data
{
    public class MyDbContext: DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<LeaderboardSnapshot> LeaderboardSnapshots { get; set; }
    }
}
