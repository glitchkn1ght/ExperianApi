using ExperianApi.Models.jsonplaceholder;
using ExperianApi.Models.Response.Common;
using System.Collections.Generic;

namespace ExperianApi.Models.Response.PhotoAlbum
{
    public class AlbumResponse : BaseResponse
    {
        public AlbumResponse()
        {
            this.Albums = new List<JP_Album>();
        }
        
        public List<JP_Album> Albums { get; set; }
    }
}
