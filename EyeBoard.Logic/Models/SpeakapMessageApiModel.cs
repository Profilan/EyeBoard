using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Models
{
    public class SpeakapMessageApiModel
    {
        [JsonProperty("_links")]
        public LinkMessage Links;

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("numLikes")]
        public int Likes { get; set; }

        [JsonProperty("EID")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("embeds")]
        public IList<Embed> Embeds { get; set; }
    }
}
