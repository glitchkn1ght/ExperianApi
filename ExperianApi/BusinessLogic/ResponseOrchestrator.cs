using ExperianApi.Interfaces;
using ExperianApi.Models.Response.PhotoAlbum;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ExperianApi.BusinessLogic
{
    public class ResponseOrchestrator : IResponseOrchestrator
    {
        private readonly ILogger<ResponseOrchestrator> Logger;
        private readonly IPhotoAlbumService AlbumService;
        private readonly IPhotoAlbumMapper PhotoAlbumMapper;

        public ResponseOrchestrator( ILogger<ResponseOrchestrator> logger, IPhotoAlbumService albumService, IPhotoAlbumMapper photoAlbumMapper)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.AlbumService = albumService ?? throw new ArgumentNullException(nameof(albumService));
            this.PhotoAlbumMapper = photoAlbumMapper ?? throw new ArgumentNullException(nameof(photoAlbumMapper));
        }

        public async Task<PhotoAlbumResponse> GetAlbumsWithPhotos(int? userId)
        {
            PhotoAlbumResponse combinedResponse= new PhotoAlbumResponse();

            AlbumResponse albumResponse = await this.AlbumService.GetAlbums(userId);
            PhotoResponse photoResponse = await this.AlbumService.GetPhotos(userId);

            if (!(albumResponse.IsSuccess) || !(photoResponse.IsSuccess))
            {
                combinedResponse.IsSuccess = false;

                combinedResponse.Message = $"Non success code received from one or both endpoints JP_Album: {albumResponse.Message}, Photo {albumResponse.Message}";

                return combinedResponse;
            }

            combinedResponse = this.PhotoAlbumMapper.MapPhotosToAlbums(albumResponse, photoResponse);

            combinedResponse.IsSuccess = true;

            return combinedResponse;
        }
    }
}
