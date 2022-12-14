using ExperianApi.Interfaces;
using ExperianApi.Models.Response.PhotoAlbum;
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get(int? userId)
        {
            PhotoAlbumResponse response = new PhotoAlbumResponse();

            try
            {
                response = await this.ResponseOrchestrator.GetAlbumsWithPhotos(userId);

                return Ok(response.Albums);
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
