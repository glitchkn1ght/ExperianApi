using ExperianApi.Models.jsonplaceholder;
using ExperianApi.Models.Photos;
using ExperianApi.Models.Response.Common;
using System.Collections.Generic;

namespace ExperianApi.Models.Response.PhotoAlbum
{
    public class PhotoResponse : BaseResponse
    {
        public PhotoResponse()
        {
            this.Photos = new List<JP_Photo>();
        }
        
        public List<JP_Photo> Photos { get; set; }
    }
}
