using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ExperianApi.Models.jsonplaceholder
{
    public class JP_Photo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("albumId")]
        public int AlbumId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }
    }
}
