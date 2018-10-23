using Newtonsoft.Json;

namespace EyeBoard.Logic.Models
{
    public class GeoLocation
    {
        [JsonProperty("lon")]
        public float Lon { get; set; }
        [JsonProperty("lat")]
        public float Lat { get; set; }
    }
}