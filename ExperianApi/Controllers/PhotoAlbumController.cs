using ExperianApi.Interfaces;
using ExperianApi.Models.Response.PhotoAlbum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ExperianApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class PhotoAlbumController : ControllerBase
    {
        private readonly ILogger<PhotoAlbumController> Logger;
        private readonly IResponseOrchestrator ResponseOrchestrator;

        public PhotoAlbumController(ILogger<PhotoAlbumController> logger, IResponseOrchestrator responseOrchestrator)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.ResponseOrchestrator = responseOrchestrator ?? throw new ArgumentNullException(nameof(responseOrchestrator));
        }

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PhotoAlbumResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PhotoAlbumResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(PhotoAlbumResponse))]
        public async Task<IActionResult> Get(int? userId)
        {
            PhotoAlbumResponse response = new PhotoAlbumResponse();
            try
            {
                if (userId == 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Validation of userId failed, please check input.";

                    return new BadRequestObjectResult(response);
                }

                string LogMsg = userId.HasValue ? $"PhotoAlbum data foruserId {userId}.": "all PhotoAlbum data.";

                this.Logger.LogInformation($"[Operation=Get(Company)], Status=Success, Message=Attempting to retrieve {LogMsg}");

                response = await this.ResponseOrchestrator.GetAlbumsWithPhotos(userId);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                this.Logger.LogError($"[Operation=Get(Company)], Status=Failed, Message=Exeception thrown: {ex.Message}");

                response.IsSuccess = false;
                response.Message = "Internal Server Error";

                return new ObjectResult(response) { StatusCode = 500 };
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return await this.Get(null);
        }
    }
}
