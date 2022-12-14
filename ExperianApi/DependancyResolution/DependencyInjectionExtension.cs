using ExperianApi.BusinessLogic;
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

            services.AddHttpClient<IPhotoAlbumService>();

            services.AddScoped<IPhotoAlbumService, PhotoAlbumService>();

            services.AddScoped<IResponseOrchestrator, ResponseOrchestrator>();

            return services;
        }
    }
}
