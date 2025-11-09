using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AggregatorService.Models
{
    public class Entry
    {

        [JsonPropertyName("character")]
        public Character Player { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("season_match_statistics")]
        public Record SeasonRecord { get; set; }


    }
}
