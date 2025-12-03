using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Leaderboard
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("entries")]
        public List<Entry> Entries { get; set; }
    }
}
