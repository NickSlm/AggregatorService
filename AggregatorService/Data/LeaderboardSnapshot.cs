using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggregatorService.Data
{
    public class LeaderboardSnapshot
    {
        public int Id { get; set; }
        public DateTime DatePulled { get; set; }
        public string Name { get; set; }
        public List<LeaderboardEntry> Entries { get; set; }
    }
}
