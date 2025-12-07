using CleanupService.Data;
using CleanupService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanupService.Services
{
    public class DbService: IDbService
    {
        private readonly MyDbContext _dbContext;

        public DbService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CleanOldRecords()
        {
            TimeSpan cutoff = TimeSpan.FromDays(100);

            var threshold = DateTime.UtcNow - cutoff;

            var snapshot = await _dbContext.LeaderboardSnapshots.Include(s => s.Entries).Where(e => e.DatePulled <= threshold).ToListAsync();

            _dbContext.LeaderboardSnapshots.RemoveRange(snapshot);
            await _dbContext.SaveChangesAsync();
        }

    }
}
