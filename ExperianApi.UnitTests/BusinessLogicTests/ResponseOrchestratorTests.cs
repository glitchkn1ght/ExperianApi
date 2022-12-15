using ExperianApi.BusinessLogic;
using ExperianApi.Interfaces;
using ExperianApi.Models.Photos;
using ExperianApi.Models.Response.PhotoAlbum;
using ExperianApi.UnitTests.TestingData;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExperianApi.UnitTests.BusinessLogicTests
{
    public class ResponseOrchestratorTests
    {
        private Mock<ILogger<ResponseOrchestrator>> LoggerMock;

        private Mock<IPhotoAlbumService> AlbumService;

        private Mock<IPhotoAlbumMapper> PhotoAlbumMapper;

        private ResponseOrchestrator ResponseOrchestrator;

        private TestData TestData;

        [SetUp]
        public void Setup()
        {
            this.TestData = new TestData();
            this.LoggerMock = new Mock<ILogger<ResponseOrchestrator>>();
            this.AlbumService = new Mock<IPhotoAlbumService>();
            this.PhotoAlbumMapper = new Mock<IPhotoAlbumMapper>();

            this.ResponseOrchestrator = new ResponseOrchestrator(this.LoggerMock.Object, this.AlbumService.Object, this.PhotoAlbumMapper.Object);
        }

        [Test]
        public void WhenBothServiceCallsSucceedAndMapperSucceeds_ReturnSuccess()
        {
            AlbumResponse albumResponse = new AlbumResponse
            {
                IsSuccess = true
            };

            PhotoResponse photoResponse = new PhotoResponse
            {
                IsSuccess = true
            };

            List<Album> MappedAlbums = new List<Album>
            {
                this.TestData.ReturnValidAlbumData()
            };

            this.AlbumService.Setup(x => x.GetAlbums(It.IsAny<int>())).Returns(Task.FromResult(albumResponse));
            this.AlbumService.Setup(x => x.GetPhotos(It.IsAny<int>())).Returns(Task.FromResult(photoResponse));
            this.PhotoAlbumMapper.Setup(x => x.MapPhotosToAlbums(It.IsAny<AlbumResponse>(), It.IsAny<PhotoResponse>())).Returns(MappedAlbums);

            PhotoAlbumResponse actual = this.ResponseOrchestrator.GetAlbumsWithPhotos(It.IsAny<int>()).Result;

            Assert.AreEqual(1, actual.Albums.Count);
            Assert.AreEqual(true, actual.IsSuccess);
            Assert.AreEqual(null, actual.Message);
        }

        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void WhenEitherServiceCallFails_ReturnNonSuccess(bool albumIsSuccess, bool photoIsSuccess)
        {
            AlbumResponse albumResponse = new AlbumResponse
            {
                IsSuccess = albumIsSuccess
            };

            PhotoResponse photoResponse = new PhotoResponse
            {
                IsSuccess = photoIsSuccess
            };

            this.AlbumService.Setup(x => x.GetAlbums(It.IsAny<int>())).Returns(Task.FromResult(albumResponse));
            this.AlbumService.Setup(x => x.GetPhotos(It.IsAny<int>())).Returns(Task.FromResult(photoResponse));
     
            PhotoAlbumResponse actual = this.ResponseOrchestrator.GetAlbumsWithPhotos(It.IsAny<int>()).Result;

            Assert.AreEqual(false, actual.IsSuccess);
            Assert.AreEqual("Non success code received from one or both endpoints, check logs for details.", actual.Message);
        }
    }
}
