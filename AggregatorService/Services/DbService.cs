using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregatorService.Data;
using AggregatorService.Interfaces;
using AggregatorService.Models;
using Microsoft.VisualBasic;

namespace AggregatorService.Services
{
    public class DbService: IDbService
    {
        private readonly MyDbContext _dbContext;
        private readonly IBlizzardApiService _apiService;

        public DbService(MyDbContext dbContext, IBlizzardApiService apiService)
        {
            _dbContext = dbContext;
            _apiService = apiService;
        }


        public async Task SaveSnapshot()
        {
            var leaderboard =  await _apiService.GetRawData();
            var snapshot = new LeaderboardSnapshot()
            {
                DatePulled = DateTime.UtcNow,
                Name = leaderboard.Name,
                Entries = leaderboard.Entries.Select(e => new LeaderboardEntry
                {
                    CharacterId = e.Character.Id,
                    CharacterName = e.Character.Name,
                    Rank = e.Rank,
                    Rating = e.Rating,
                    Played = e.SeasonMatchStatistics.Played,
                    Won = e.SeasonMatchStatistics.Won,
                    Lost = e.SeasonMatchStatistics.Lost

                }).ToList()
            };
            _dbContext.Add(snapshot);
            await _dbContext.SaveChangesAsync();
        }
    }
}
