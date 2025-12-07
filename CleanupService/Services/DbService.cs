using CleanupService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanupService.Services
{
    public class DbService
    {
        private readonly MyDbContext _dbContext;

        public DbService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CleanupOldRecords()
        {

        }

    }
}
