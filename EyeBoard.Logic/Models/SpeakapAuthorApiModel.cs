using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Models
{

    public class SpeakapAuthorApiModel
    {
        [JsonProperty("_links")]
        public LinkAuthor Links { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }
    }
}
