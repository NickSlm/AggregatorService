using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data
{
    public class LeaderboardEntry
    {
        public int Id { get; set; }
        public string CharacterName { get; set; }
        public int CharacterId { get; set; }
        public int Rank { get; set; }
        public int Rating { get; set; }
        public int Played { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }

        public int SnapshotId { get; set; }
        public LeaderboardSnapshot Snapshot { get; set; }



    }
}
