using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AggregatorService.Models
{
    public class Record
    {
        [Key, ForeignKey("Entry")]
        public int Id { get; set; }

        [JsonPropertyName("played")]
        public int Played { get; set; }

        [JsonPropertyName("won")]
        public int Won { get; set; }

        [JsonPropertyName("lost")]
        public int Lost { get; set; }
    }
}
