using ExperianApi.Models.Photos;
using ExperianApi.Models.Response.PhotoAlbum;
using System.Collections.Generic;

namespace ExperianApi.Interfaces
{
    public interface IPhotoAlbumMapper
    {
        public List<Album> MapPhotosToAlbums(AlbumResponse albumResponse, PhotoResponse photoResponse);
    }
}
