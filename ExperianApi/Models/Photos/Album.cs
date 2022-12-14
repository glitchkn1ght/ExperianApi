using ExperianApi.Models.jsonplaceholder;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ExperianApi.Models.Photos
{
    public class Album
    {
        public Album()
        {
            this.AlbumPhotos = new List<Photo>();
        }

        public int AlbumId { get; set; }

        public int UserId { get; set; }

        public string AlbumTitle { get; set; }

        public List<Photo> AlbumPhotos { get; set; }
    }
}
