using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregatorService.Interfaces;
using Microsoft.VisualBasic;

namespace AggregatorService.Services
{
    public class DbService: IDbService
    {


        public async Task InitializeAsync()
        {
            using var db = new MyDbContext();

        }

    }
}
