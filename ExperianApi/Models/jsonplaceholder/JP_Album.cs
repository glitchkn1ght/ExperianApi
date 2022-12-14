using Newtonsoft.Json;

namespace ExperianApi.Models.jsonplaceholder
{
    public class JP_Album
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
