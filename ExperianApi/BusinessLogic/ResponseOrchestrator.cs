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
            PhotoAlbumResponse photoAlbumResponse= new PhotoAlbumResponse();

            AlbumResponse albumResponse = await this.AlbumService.GetAlbums(userId);
            PhotoResponse photoResponse = await this.AlbumService.GetPhotos(userId);

            if (!(albumResponse.IsSuccess) || !(photoResponse.IsSuccess))
            {
                photoAlbumResponse.IsSuccess = false;

                photoAlbumResponse.Message = $"Non success code received from one or both endpoints JP_Album: Check logs for details.";

                return photoAlbumResponse;
            }

            photoAlbumResponse = this.PhotoAlbumMapper.MapPhotosToAlbums(albumResponse, photoResponse);

            photoAlbumResponse.IsSuccess = true;

            return photoAlbumResponse;
        }
    }
}
