using ExperianApi.BusinessLogic;
using ExperianApi.ClientHandler;
using ExperianApi.Interfaces;
using ExperianApi.Mappers;
using Microsoft.Extensions.DependencyInjection;
using WeatherWindowsService.WeatherApi.Service;

namespace ExperianApi.DependancyResolution
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPhotoAlbumMapper, PhotoAlbumMapper>();

            services.AddHttpClient<IHttpClientWrapper>();

            services.AddScoped<IHttpClientWrapper, HttpClientWrapper>();

            services.AddScoped<IPhotoAlbumService, PhotoAlbumService>();

            services.AddScoped<IResponseOrchestrator, ResponseOrchestrator>();

            return services;
        }
    }
}
