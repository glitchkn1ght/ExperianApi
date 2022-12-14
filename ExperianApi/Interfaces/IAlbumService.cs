using ExperianApi.Models.Response.PhotoAlbum;
using System.Threading.Tasks;

namespace ExperianApi.Interfaces
{
    public interface IPhotoAlbumService
    {
        public Task<AlbumResponse> GetAlbums(int? userId);

        public Task<PhotoResponse> GetPhotos(int? userId);
    }

}
