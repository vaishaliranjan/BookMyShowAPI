/*using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository;
using BookMyShow.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace BookMyShow.Tests.Repository.Tests
{
    [TestClass]
    public class ArtistRepositoryTests
    {
        private Mock<AppDbContext> _mockDbContext;
        private DbContextOptions<AppDbContext> options;
        private AppDbContext dbContext;

        [TestInitialize]
        public void SetUp()
        {
            _mockDbContext = new Mock<AppDbContext>();
            options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "InMemoryDatabase").Options;
            dbContext = new AppDbContext(options);
            dbContext.Artists.Add(new Artist() { Id = 1, Name = "Arijit Singh", IsBooked = false });
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void GetAllArtists_GetRequest_ReturnsArtistList()
        {
            var artistRepository = new ArtistRepository(dbContext);
            var result = artistRepository.GetAllArtists();

            Assert.IsNotNull(result);
            Assert.AreEqual(1,result.Count());
        }

        [TestMethod]
        public void AddArtist_InputArtist()
        {
            var artistRepository = new ArtistRepository(dbContext);
            artistRepository.AddArtist(new Artist());

            Assert.AreEqual(2, dbContext.Artists.Count());
            
        }

        [TestMethod]
        public void UpdateArtist_InputArtistWithBookedTrue_ArtistFound()
        {

            var artistRepository = new ArtistRepository(dbContext);
            artistRepository.UpdateArtist(new Artist() {Id=1 ,Name="Arijit Singh", IsBooked=true});

            Assert.IsTrue(dbContext.Artists.FirstOrDefault(a=>a.Id==1).IsBooked);
        }


    }
    
}
*/