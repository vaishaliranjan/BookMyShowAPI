/*using BookMyShow.Business;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyShow.Tests.Business.Tests
{
    [TestClass]
    public class VenueBusinessTests
    {
        private Mock<IVenueRepository> _mockVenueRepository;
        private List<Venue> venueList;


        [TestInitialize]
        public void SetUp()
        {
            _mockVenueRepository = new Mock<IVenueRepository>();
            venueList = new List<Venue>()
            {
                new Venue(){VenueId=1,Place="GIP", IsBooked=false},
                new Venue(){VenueId=2,Place="GIPIndia", IsBooked=true}
            };

        }

        [TestMethod]
        public void GetAllVenues_GetRequest_ReturnsListOfVenues()
        {
            _mockVenueRepository.Setup(v => v.GetAllVenues()).Returns(venueList);
            var venueBusiness = new VenueBusiness(_mockVenueRepository.Object);
            var result = venueBusiness.GetAllVenues();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetArtist_GetRequest_ReturnsArtist()
        {
            _mockVenueRepository.Setup(v => v.GetAllVenues()).Returns(venueList);
            var venueBusiness = new VenueBusiness(_mockVenueRepository.Object);

            var result = venueBusiness.GetVenue(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Venue));
        }

        [TestMethod]
        public void GetArtist_GetRequest_ReturnsNull()
        {
            _mockVenueRepository.Setup(v => v.GetAllVenues()).Returns(venueList);
            var venueBusiness = new VenueBusiness(_mockVenueRepository.Object);

            var result = venueBusiness.GetVenue(3);

            Assert.IsNull(result);
        }



        [TestMethod]
        public void CreateVenue_InputVenue_AddVenue()
        {
            _mockVenueRepository.Setup(v => v.AddVenue(It.IsAny<Venue>()));
            var venueBusiness = new VenueBusiness(_mockVenueRepository.Object);

            venueBusiness.CreateVenue(venueList[0]);

            Assert.IsInstanceOfType(venueList[0], typeof(Venue));   
        }

        [TestMethod]
        public void BookVenue_InputVenueId_ReturnsTrue()
        {
            _mockVenueRepository.Setup(v => v.GetAllVenues()).Returns(venueList);
            _mockVenueRepository.Setup(v => v.UpdateVenue(It.IsAny<Venue>()));
            var venueBusiness = new VenueBusiness(_mockVenueRepository.Object);

            var result = venueBusiness.BookVenue(1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BookVenue_InputVenueId_ReturnsFalseForBookedVenue()
        {
            _mockVenueRepository.Setup(v => v.GetAllVenues()).Returns(venueList);
            _mockVenueRepository.Setup(v => v.UpdateVenue(It.IsAny<Venue>()));
            var venueBusiness = new VenueBusiness(_mockVenueRepository.Object);

            var result = venueBusiness.BookVenue(2);

            Assert.IsFalse(result);
        }


        [TestMethod]
        public void BookVenue_InputVenueId_ReturnsFalseForInvalidVenue()
        {
            _mockVenueRepository.Setup(v => v.GetAllVenues()).Returns(venueList);
            _mockVenueRepository.Setup(v => v.UpdateVenue(It.IsAny<Venue>()));
            var venueBusiness = new VenueBusiness(_mockVenueRepository.Object);

            var result = venueBusiness.BookVenue(3);

            Assert.IsFalse(result);
        }


        [TestMethod]
        public void UnbookVenue_InputVenueId_ReturnsTrue()
        {
            _mockVenueRepository.Setup(v => v.GetAllVenues()).Returns(venueList);
            _mockVenueRepository.Setup(v => v.UpdateVenue(It.IsAny<Venue>()));
            var venueBusiness = new VenueBusiness(_mockVenueRepository.Object);

            var result = venueBusiness.UnBookVenue(2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UnbookVenue_InputVenueId_ReturnsFalseForUnbookedVenue()
        {
            _mockVenueRepository.Setup(v => v.GetAllVenues()).Returns(venueList);
            _mockVenueRepository.Setup(v => v.UpdateVenue(It.IsAny<Venue>()));
            var venueBusiness = new VenueBusiness(_mockVenueRepository.Object);

            var result = venueBusiness.UnBookVenue(1);

            Assert.IsFalse(result);
        }


        [TestMethod]
        public void UnbookVenue_InputVenueId_ReturnsFalseForInvalidVenue()
        {
            _mockVenueRepository.Setup(v => v.GetAllVenues()).Returns(venueList);
            _mockVenueRepository.Setup(v => v.UpdateVenue(It.IsAny<Venue>()));
            var venueBusiness = new VenueBusiness(_mockVenueRepository.Object);

            var result = venueBusiness.UnBookVenue(3);

            Assert.IsFalse(result);
        }

    }
}
*/