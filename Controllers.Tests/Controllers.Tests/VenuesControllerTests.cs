/*using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Controllers;
using BookMyShow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BookMyShow.Tests.Controllers.Tests
{
    [TestClass]
    public class VenuesControllerTests
    {
        public static List<Venue> venueList;
        private Mock<IVenueBusiness> _mockvenueBusiness;

        [TestInitialize]
        public void Setup()
        {
            _mockvenueBusiness = new Mock<IVenueBusiness>();
            venueList = new List<Venue>()
            {
                new Venue(){Place="GIP", IsBooked=false},
                new Venue(){ Place="DLF", IsBooked= false}
            };

        }


        [TestMethod]
        public void GetVenueDetails_InputVenueId_Returns404NotFound()
        {
            int venueId = 1;
            _mockvenueBusiness.Setup(v => v.GetVenue(venueId)).Returns<Venue>(null);
            var venueController = new VenuesController(_mockvenueBusiness.Object);

            var response = venueController.Get(venueId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetVenueDetails_InputVenueId_Returns200Ok()
        {
            int venueId = 1;
            _mockvenueBusiness.Setup(v => v.GetVenue(venueId)).Returns(venueList[0]);
            var venueController = new VenuesController(_mockvenueBusiness.Object);

            var response = venueController.Get(venueId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void GetAllVenueDetails_GetRequest_Returns404NotFound()
        {
            int? venueId = null;
            _mockvenueBusiness.Setup(v => v.GetAllVenues()).Returns<List<Venue>>(null);
            var venueController = new VenuesController(_mockvenueBusiness.Object);

            var response = venueController.Get(venueId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetAllVenueDetails_GetRequest_Returns200Ok()
        {
            int? venueId = null;
            _mockvenueBusiness.Setup(v => v.GetAllVenues()).Returns(venueList);
            var venueController = new VenuesController(_mockvenueBusiness.Object);

            var response = venueController.Get(venueId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void GetAllVenueDetails_GetRequest_Returns500InternalServerError()
        {
            int? venueId = null;
            _mockvenueBusiness.Setup(v => v.GetAllVenues()).Throws(new Exception());
            var venueController = new VenuesController(_mockvenueBusiness.Object);

            var response = venueController.Get(venueId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void GetArtistDetails_InputArtistId_Returns500InternalServerError()
        {
            int venueId = 1;
            _mockvenueBusiness.Setup(v => v.GetVenue(venueId)).Throws(new Exception());
            var venueController = new VenuesController(_mockvenueBusiness.Object);

            var response = venueController.Get(venueId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void PostVenue_InputVenue_ReturnsOk()
        {
            var venue = venueList[0];
            _mockvenueBusiness.Setup(v => v.CreateVenue(venue));
            var venuesController = new VenuesController(_mockvenueBusiness.Object);

            var response = venuesController.Post(venue) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void PostVenue_InputVenue_Returns500InternalServerError()
        {
            var venue = venueList[0];
            _mockvenueBusiness.Setup(v => v.CreateVenue(venue)).Throws(new Exception());
            var venuesController = new VenuesController(_mockvenueBusiness.Object);

            var response = venuesController.Post(venue) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void PostVenue_InputVenue_Returns400BadRequestModelError()
        {
            var venue = venueList[0];
            var venuesController = new VenuesController(_mockvenueBusiness.Object);
            venuesController.ModelState.AddModelError("Place", "Missing Place");
            var response = venuesController.Post(venue) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);

        }
    }
}
*/