using ExperianApi.Interfaces;
using ExperianApi.Models.jsonplaceholder;
using ExperianApi.Models.Response.PhotoAlbum;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherWindowsService.WeatherApi.Configuration;

namespace WeatherWindowsService.WeatherApi.Service
{
    public class PhotoAlbumService : IPhotoAlbumService
    {
        private readonly ILogger<PhotoAlbumService> Logger;
        private readonly ApiSettings ApiSettings;
        private readonly IHttpClientWrapper ClientWrapper;

        public PhotoAlbumService(
                ILogger<PhotoAlbumService> logger,  
                IOptions<ApiSettings> apiSettings,
                IHttpClientWrapper clientWrapper)
        {

            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.ApiSettings = apiSettings.Value;
            this.ClientWrapper = clientWrapper ?? throw new ArgumentNullException(nameof(clientWrapper));
            this.ClientWrapper.SetBaseAddress(new Uri(ApiSettings.BaseURL));
        }

        public async Task<AlbumResponse> GetAlbums(int? userId)
        {
            AlbumResponse albumResponse = new AlbumResponse();

            var resource = $"{ApiSettings.AlbumResource}";

            if(userId.HasValue && userId > 0)
            {
                resource += $"?userId={userId}";
            }

            var rawResponse = await this.ClientWrapper.GetAsync(resource);

            if (rawResponse.IsSuccessStatusCode)
            {
                Logger.LogInformation($"[Operation=GetAlbums(PhotoAlbumService)], Status=Success, Message=Success code received from album endpoint, mapping data.");

                albumResponse.Albums = JsonConvert.DeserializeObject<List<JP_Album>>(await rawResponse.Content.ReadAsStringAsync());

                albumResponse.IsSuccess = true;
            }
            else
            {
                Logger.LogWarning($"[Operation=GetAlbums(PhotoAlbumService)], Status=Failure, Message=Non success code received from album endpoint.");

                albumResponse.IsSuccess = false;
            }

            return albumResponse;
        }

        public async Task<PhotoResponse> GetPhotos(int? userId)
        {
            PhotoResponse photoResponse = new PhotoResponse();

            var resource = $"{ApiSettings.PhotoResource}";

            if (userId.HasValue && userId > 0)
            {
                resource += $"?userId={userId}";
            }

            var rawResponse = await this.ClientWrapper.GetAsync(resource);

            if (rawResponse.IsSuccessStatusCode)
            {
                Logger.LogInformation($"[Operation=GetPhotos(PhotoAlbumService)], Status=Success, Message=Success code received from photo endpoint, mapping data.");

                photoResponse.Photos = JsonConvert.DeserializeObject<List<JP_Photo>>(await rawResponse.Content.ReadAsStringAsync());

                photoResponse.IsSuccess = true;
            }
            else
            {
                Logger.LogWarning($"[Operation=GetPhotos(PhotoAlbumService)], Status=Failure, Message=Non success code received from photo endpoint.");

                photoResponse.IsSuccess = false;
            }

            return photoResponse;
        }
    }
}
