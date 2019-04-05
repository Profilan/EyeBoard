using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Models
{
    public class LinkHref
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class LinkMessages
    {
        [JsonProperty("self")]
        public LinkHref Self { get; set; }

        [JsonProperty("messages")]
        public IList<LinkHref> Messages { get; set; }

        public LinkMessages()
        {
            Messages = new List<LinkHref>();
        }
    }

    public class LinkMessage
    {
        [JsonProperty("self")]
        public LinkHref Self { get; set; }

        [JsonProperty("author")]
        public LinkHref Author { get; set; }

        [JsonProperty("images")]
        public IList<LinkHref> Images { get; set; }
    }

    public class LinkAuthor
    {
        [JsonProperty("self")]
        public LinkHref Self { get; set; }

        [JsonProperty("avatar")]
        public LinkHref Avatar { get; set; }
    }

}
