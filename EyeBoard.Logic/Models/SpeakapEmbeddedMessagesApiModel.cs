
using Newtonsoft.Json;

namespace EyeBoard.Logic.Models
{
    public class SpeakapEmbeddedMessagesApiModel
    {
        [JsonProperty("_embedded")]
        public EmbeddedMessages Embedded { get; set; }
    }
}
