using AggregatorService.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggregatorService
{
    public class MyDbContext: DbContext
    {

        private readonly string _databasePath;
        private readonly string _localFolder;
        private readonly string _appFolder;

        public DbSet<Entry> Entries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                options.UseSqlite($"Data Source={_databasePath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // If Entry has owned types (Character, Record)
            modelBuilder.Entity<Entry>().OwnsOne(e => e.Player);
            modelBuilder.Entity<Entry>().OwnsOne(e => e.SeasonRecord);
        }
    }
}
