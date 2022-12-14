using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ExperianApi.Models.Photos
{
    public class Photo
    {
        public int PhotoId { get; set; }

        public int AlbumId { get; set; }

        public string PhotoTitle { get; set; }

        public string PhotoUrl { get; set; }

        public string PhotoThumbnailUrl { get; set; }
    }
}
