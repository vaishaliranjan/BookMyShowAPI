using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace BookMyShow.Tests.Repository.Tests
{
    [TestClass]
    public class VenueRepositoryTests
    {
        private Mock<AppDbContext> _mockDbContext;
        private DbContextOptions<AppDbContext> options;
        private AppDbContext dbContext;
        private VenueRespository venueRespository;

        [TestInitialize]
        public void SetUp()
        {
            _mockDbContext = new Mock<AppDbContext>();
            options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "InMemoryDatabase").Options;
            dbContext = new AppDbContext(options);
            venueRespository= new VenueRespository(dbContext);
            dbContext.Venues.Add(new Venue() { VenueId = 1, Place="Gip", IsBooked = false });
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void GetAllVenues_GetRequest_ReturnsVenueList()
        {
            var result = venueRespository.GetAllVenues();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void AddVenue_InputVenue()
        {
            venueRespository.AddVenue(new Venue());
            Assert.AreEqual(2, dbContext.Venues.Count());

        }

        [TestMethod]
        public void UpdateVenue_InputVenue_VenueFound()
        {
            venueRespository.UpdateVenue(new Venue() { VenueId = 1, Place="Gip", IsBooked = true });
            Assert.IsTrue(dbContext.Venues.Find(1).IsBooked);
        }
    }
}
