using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Models
{
    public class SpeakapGroupsApiModel
    {
        [JsonProperty("_links")]
        public LinkGroups Links { get; set; }
    }
}
