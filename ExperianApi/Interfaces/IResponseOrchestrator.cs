using ExperianApi.Models.Response.PhotoAlbum;
using System.Threading.Tasks;

namespace ExperianApi.Interfaces
{
    public interface IResponseOrchestrator
    {
        public Task<PhotoAlbumResponse> GetAlbumsWithPhotos(int? userId);
    }
}