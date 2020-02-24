using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Models
{
    public class EventApiModel
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Subject")]
        public string Subject { get; set; }

        [JsonProperty("IsAllDay")]
        public bool IsAllDay { get; set; }

        [JsonProperty("IsCancelled")]
        public bool IsCancelled { get; set; }

        [JsonProperty("Start")]
        public EventApiTime Start { get; set; }

        [JsonProperty("End")]
        public EventApiTime End { get; set; }
    }
}
