using ExperianApi.Controllers;
using ExperianApi.Interfaces;
using ExperianApi.Models.Photos;
using ExperianApi.Models.Response.PhotoAlbum;
using ExperianApi.UnitTests.TestingData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExperianApi.UnitTests.ControllerTests
{
    public class PhotoAlbumControllerTests
    {
        private Mock<ILogger<PhotoAlbumController>> LoggerMock;

        private Mock<IResponseOrchestrator> ResponseOrchestratorMock;

        private PhotoAlbumController PhotoAlbumController;

        private TestData TestData;

        [SetUp]
        public void Setup()
        {
            this.TestData = new TestData();
            this.LoggerMock = new Mock<ILogger<PhotoAlbumController>>();
            this.ResponseOrchestratorMock = new Mock<IResponseOrchestrator>();
            this.PhotoAlbumController = new PhotoAlbumController (this.LoggerMock.Object,this.ResponseOrchestratorMock.Object);
        }

        [Test]
        public void WhenConstructorCalledWithNullLogger_ThenArgNullExceptionThrown()
        {
            Assert.Throws(
                Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("logger"), delegate
                {
                    this.PhotoAlbumController = new PhotoAlbumController(
                        null,
                        this.ResponseOrchestratorMock.Object
                    );
                });
        }

        [Test]
        public void WhenConstructorCalledWithNullStringInputValidator_ThenArgNullExceptionThrown()
        {
            Assert.Throws(
                Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("responseOrchestrator"), delegate
                {
                    this.PhotoAlbumController = new PhotoAlbumController(
                        this.LoggerMock.Object,
                         null
                    );
                });
        }

        [Test]
        public void WhenConstructorCalledWithValidArguements_ThenNoExceptionThrown()
        {
            Assert.DoesNotThrow(
                delegate
                {
                    this.PhotoAlbumController = new PhotoAlbumController
                    (
                        this.LoggerMock.Object,
                        this.ResponseOrchestratorMock.Object
                    );
                });
        }

        [Test]
        public void WhenResponseOrchestratorReturnsSuccess_ThenReturn200WithData()
        {
            PhotoAlbumResponse expectedFromOrchestrator = new PhotoAlbumResponse
            {
                IsSuccess = true,
                Albums = new List<Album>
                {
                    this.TestData.ReturnValidAlbumData()
                }

            };
            
            this.ResponseOrchestratorMock.Setup(x => x.GetAlbumsWithPhotos(It.IsAny<int>())).Returns(Task.FromResult(expectedFromOrchestrator));

            ObjectResult expected = new OkObjectResult(expectedFromOrchestrator);


            ObjectResult actual = (ObjectResult)this.PhotoAlbumController.Get(1).Result;

            Assert.IsInstanceOf<PhotoAlbumResponse>(actual.Value);
            Assert.AreEqual(200, actual.StatusCode);
            Assert.AreEqual(true, ((PhotoAlbumResponse)actual.Value).IsSuccess);
        }

        [Test]
        public void WhenResponseOrchestratorReturnsNonSuccess_ThenReturn500WithMessage()
        {
            PhotoAlbumResponse expectedFromOrchestrator = new PhotoAlbumResponse
            {
                IsSuccess = false,
                Message = "Non success code received from one or both endpoints, check logs for details."
            };

            this.ResponseOrchestratorMock.Setup(x => x.GetAlbumsWithPhotos(It.IsAny<int>())).Returns(Task.FromResult(expectedFromOrchestrator));

            ObjectResult expected = new ObjectResult(expectedFromOrchestrator) { StatusCode = 500 };


            ObjectResult actual = (ObjectResult)this.PhotoAlbumController.Get(1).Result;

            Assert.IsInstanceOf<PhotoAlbumResponse>(actual.Value);
            Assert.AreEqual(500, actual.StatusCode);
            Assert.AreEqual(false, ((PhotoAlbumResponse)actual.Value).IsSuccess);
        }
    }
}
