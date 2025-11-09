using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AggregatorService.Models
{
    public class Record
    {
        [JsonPropertyName("played")]
        public int Played { get; set; }

        [JsonPropertyName("won")]
        public int Won { get; set; }

        [JsonPropertyName("lost")]
        public int Lost { get; set; }
    }
}
