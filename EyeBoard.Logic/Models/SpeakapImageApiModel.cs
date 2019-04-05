using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Models
{
    public class SpeakapDisplayUrls
    {
        [JsonProperty("timelinePreviewSmall")]
        public string TimelinePreviewSmall { get; set; }

        [JsonProperty("fullscreen1080p")]
        public string Fullscreen1080p { get; set; }
    }

    public class SpeakapFile
    {
        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("displayUrls")]
        public SpeakapDisplayUrls DisplayUrls { get; set; }
    }

    public class SpeakapImageApiModel
    {
        [JsonProperty("file")]
        public SpeakapFile SpeakapFile { get; set; }
    }
}
