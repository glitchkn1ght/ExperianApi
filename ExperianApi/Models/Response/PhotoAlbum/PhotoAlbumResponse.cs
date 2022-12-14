using ExperianApi.Models.Photos;
using ExperianApi.Models.Response.Common;
using System.Collections.Generic;

namespace ExperianApi.Models.Response.PhotoAlbum
{
    public class PhotoAlbumResponse : BaseResponse
    {
        public PhotoAlbumResponse()
        {
            this.Albums = new List<Album>();
        }
        
        public List<Album> Albums { get; set; }
    }
}
