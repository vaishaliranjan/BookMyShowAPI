/*using BookMyShow.Business;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace BookMyShow.Tests.Business.Tests
{
    [TestClass]
    public class ArtistBusinessTests
    {
        private Mock<IArtistRepository> _mockArtistRepository;
        private List<Artist> artistList;
        

        [TestInitialize]
        public void SetUp()
        {
                _mockArtistRepository = new Mock<IArtistRepository>();
                artistList = new List<Artist>()
            {
                new Artist(){Id=1,Name="ArijitSingh", Timing="12-12-2023T03:00:00",IsBooked=false},
                new Artist() { Id=2,Name="Ankit Singh",Timing="01-01-2024T03:00:00",IsBooked=true}
            };
            
        }

        [TestMethod]
        public void GetAllArtist_GetRequest_ReturnsListOfArtist()
        {
            _mockArtistRepository.Setup(a => a.GetAllArtists()).Returns(artistList);
            var artistBusiness = new ArtistBusiness(_mockArtistRepository.Object);
            var result = artistBusiness.GetAllArtists();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetArtist_GetRequest_ReturnsArtist()
        {
            _mockArtistRepository.Setup(a => a.GetAllArtists()).Returns(artistList);
            var artistBusiness = new ArtistBusiness(_mockArtistRepository.Object);
            var result = artistBusiness.GetArtist(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Artist));
        }

        [TestMethod]
        public void GetArtist_GetRequest_ReturnsNull()
        {
            _mockArtistRepository.Setup(a => a.GetAllArtists()).Returns(artistList);
            var artistBusiness = new ArtistBusiness(_mockArtistRepository.Object);
            var result = artistBusiness.GetArtist(3);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateArtist_InputArtist_ReturnTrue()
        {
            _mockArtistRepository.Setup(a => a.AddArtist(It.IsAny<Artist>()));
            var artistBusiness = new ArtistBusiness(_mockArtistRepository.Object);

            var result = artistBusiness.CreateArtist(artistList[1]);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateArtist_InputArtist_ReturnsFalsePastDate()
        {
            _mockArtistRepository.Setup(a => a.AddArtist(It.IsAny<Artist>()));
            var artistBusiness = new ArtistBusiness(_mockArtistRepository.Object);

            var result = artistBusiness.CreateArtist(artistList[0]);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateArtist_InputArtist_ReturnsFalseInvalidDate()
        {
            _mockArtistRepository.Setup(a => a.AddArtist(It.IsAny<Artist>()));
            var artistBusiness = new ArtistBusiness(_mockArtistRepository.Object);

            var result = artistBusiness.CreateArtist(new Artist() { Timing="122-12-2023T05:33:71"});

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BookArtist_InputArtistId_ReturnsTrue()
        {
            _mockArtistRepository.Setup(a => a.GetAllArtists()).Returns(artistList);
            _mockArtistRepository.Setup(a => a.UpdateArtist(It.IsAny<Artist>()));
            var artistBusiness = new ArtistBusiness(_mockArtistRepository.Object);

            var result = artistBusiness.BookArtist(1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BookArtist_InputArtistId_ReturnsFalseForBookedArtist()
        {
            _mockArtistRepository.Setup(a => a.GetAllArtists()).Returns(artistList);
            _mockArtistRepository.Setup(a => a.UpdateArtist(It.IsAny<Artist>()));
            var artistBusiness = new ArtistBusiness(_mockArtistRepository.Object);

            var result = artistBusiness.BookArtist(2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BookArtist_InputArtistId_ReturnsFalseForInvalidInput()
        {
            _mockArtistRepository.Setup(a => a.GetAllArtists()).Returns(artistList);
            _mockArtistRepository.Setup(a => a.UpdateArtist(It.IsAny<Artist>()));
            var artistBusiness = new ArtistBusiness(_mockArtistRepository.Object);

            var result = artistBusiness.BookArtist(3);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UnbookArtist_InputArtistId_ReturnsTrue()
        {
            _mockArtistRepository.Setup(a => a.GetAllArtists()).Returns(artistList);
            _mockArtistRepository.Setup(a => a.UpdateArtist(It.IsAny<Artist>()));
            var artistBusiness = new ArtistBusiness(_mockArtistRepository.Object);

            var result = artistBusiness.UnBookArtist(2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UnbookArtist_InputArtistId_ReturnsFalseForUnbookedArtist()
        {
            _mockArtistRepository.Setup(a => a.GetAllArtists()).Returns(artistList);
            _mockArtistRepository.Setup(a => a.UpdateArtist(It.IsAny<Artist>()));
            var artistBusiness = new ArtistBusiness(_mockArtistRepository.Object);

            var result = artistBusiness.UnBookArtist(1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UnbookArtist_InputArtistId_ReturnsFalseForInvalidArtist()
        {
            _mockArtistRepository.Setup(a => a.GetAllArtists()).Returns(artistList);
            _mockArtistRepository.Setup(a => a.UpdateArtist(It.IsAny<Artist>()));
            var artistBusiness = new ArtistBusiness(_mockArtistRepository.Object);

            var result = artistBusiness.UnBookArtist(3);

            Assert.IsFalse(result);
        }
    }
}
*/