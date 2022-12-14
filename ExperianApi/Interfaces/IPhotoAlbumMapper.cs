using ExperianApi.Models.Response.PhotoAlbum;

namespace ExperianApi.Interfaces
{
    public interface IPhotoAlbumMapper
    {
        public PhotoAlbumResponse MapPhotosToAlbums(AlbumResponse albumResponse, PhotoResponse photoResponse);
    }
}
