using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Models
{
    public class SpeakapMessagesApiModel
    {
        [JsonProperty("_links")]
        public LinkMessages Links { get; set; }

        [JsonProperty("_embedded")]
        public EmbeddedMessages Embedded { get; set; }
    }
}
