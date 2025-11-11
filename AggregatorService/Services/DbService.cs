using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregatorService.Data;
using AggregatorService.Interfaces;
using Microsoft.VisualBasic;

namespace AggregatorService.Services
{
    public class DbService: IDbService
    {
        private readonly MyDbContext _dbContext;


        public DbService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
