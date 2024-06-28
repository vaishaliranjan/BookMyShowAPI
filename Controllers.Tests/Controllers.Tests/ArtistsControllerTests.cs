/*using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using BookMyShow.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
namespace BookMyShow.Tests.Controllers.Tests
{
    [TestClass]
    public class ArtistsControllerTests
    {

        public static List<Artist> artistList;
        public Mock<IArtistBusiness> _mockArtistBusiness;

        [TestInitialize]
        public void InitializeArtist()
        {
            _mockArtistBusiness = new Mock<IArtistBusiness>();

            artistList = new List<Artist>()
        {
            new Artist() {
                Name="Arijit Singh",
                Timing="25-12-2023T04:00:00"
            },
            new Artist() {
                Name="Arijit Singh",
                Timing="25-12-2023T04:00:00"
            }
        };
        }

        [TestMethod]
        public void GetArtistDetails_InputArtistId_Returns404NotFound()
        {
            Artist artist = null;
            int artistId = 1;
            _mockArtistBusiness.Setup(a => a.GetArtist(It.IsAny<int?>())).Returns(artist);
            var artistController = new ArtistsController(_mockArtistBusiness.Object);

            var response = artistController.Get(artistId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }


        [TestMethod]
        public void GetArtistDetails_InputArtistId_Returns200Ok()
        {

            int artistId = 0;
            _mockArtistBusiness.Setup(a => a.GetArtist(artistId)).Returns(artistList[0]);
            var artistController = new ArtistsController(_mockArtistBusiness.Object);

            var response = artistController.Get(artistId) as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void GetAllArtistDetails_GetRequest_Returns404NotFound()
        {
            int? artistId = null;
            _mockArtistBusiness.Setup(a => a.GetAllArtists()).Returns<Artist>(null);
            var artistController = new ArtistsController(_mockArtistBusiness.Object);

            var response = artistController.Get(artistId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetAllArtistDetails_GetRequest_Returns200Ok()
        {
            int? artistId = null;
            _mockArtistBusiness.Setup(a => a.GetAllArtists()).Returns(artistList);
            var artistController = new ArtistsController(_mockArtistBusiness.Object);

            var response = artistController.Get(artistId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void GetAllArtistDetails_GetRequest_Returns500InternalServerError()
        {
            int? artistId = null;
            _mockArtistBusiness.Setup(a => a.GetAllArtists()).Throws(new Exception());
            var artistController = new ArtistsController(_mockArtistBusiness.Object);

            var response = artistController.Get(artistId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void GetArtistDetails_InputArtistId_Returns500InternalServerError()
        {
            int artistId = 1;
            _mockArtistBusiness.Setup(a => a.GetArtist(artistId)).Throws(new Exception());
            var artistController = new ArtistsController(_mockArtistBusiness.Object);

            var response = artistController.Get(artistId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void PostArtist_InputArtist_Returns200Ok()
        {
            var artist = new Artist { Name = "Arijit Singh", Timing = "26-12-2023", IsBooked = false };
            _mockArtistBusiness.Setup(a => a.CreateArtist(artist)).Returns(true);

            var artistController = new ArtistsController(_mockArtistBusiness.Object);

            var response = artistController.Post(artist) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void PostArtist_InputArtist_Returns400BadRequestBadTiming()
        {
            var artist = new Artist { Name = "Arijit Singh", Timing = "22-12-2023", IsBooked = false };
            _mockArtistBusiness.Setup(a => a.CreateArtist(artist)).Returns(false);

            var artistController = new ArtistsController(_mockArtistBusiness.Object);

            var response = artistController.Post(artist) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void PostArtist_InputArtist_Returns500InternalServerError()
        {
            var artist = new Artist { Name = "Arijit Singh", Timing = "22-12-2023", IsBooked = false };
            _mockArtistBusiness.Setup(a => a.CreateArtist(artist)).Throws(new Exception());

            var artistController = new ArtistsController(_mockArtistBusiness.Object);

            var response = artistController.Post(artist) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void PostArtist_InputArtist_Returns400BadRequestModelError()
        {
            var artist = new Artist { Name = "Arijit Singh", Timing = "22-12-2023", IsBooked = false };

            var artistController = new ArtistsController(_mockArtistBusiness.Object);
            artistController.ModelState.AddModelError("Name", "Missing Name");

            var response = artistController.Post(artist) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);

        }
    }
}
*/