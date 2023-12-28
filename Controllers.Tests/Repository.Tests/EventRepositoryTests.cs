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
    public class EventRepositoryTests
    {
        private Mock<AppDbContext> _mockDbContext;
        private DbContextOptions<AppDbContext> options;
        private AppDbContext dbContext;
        private EventRepository eventRepository;
        [TestInitialize]
        public void SetUp()
        {
            _mockDbContext = new Mock<AppDbContext>();
            options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "InMemoryDatabase").Options;
            dbContext = new AppDbContext(options);
            eventRepository = new EventRepository(dbContext);
            dbContext.Events.Add(new Event() { Id = 1, NumberOfTickets = 100 });
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void GetAllEvents_GetRequest_ReturnsEventList()
        {
            var result = eventRepository.GetAllEvents();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void AddEvent_InputEvent()
        {
            eventRepository.AddEvent(new Event());

            Assert.AreEqual(2, dbContext.Events.Count());
        }

        [TestMethod]
        public void RemoveEvent_InputEvent_FoundEvent()
        {
            eventRepository.RemoveEvent(new Event() { Id=1});

            Assert.AreEqual(0, dbContext.Events.Count());
        }

        [TestMethod]
        public void UpdateEvent_InputEvent_FoundEvent()
        {
            eventRepository.UpdateEvent(new Event() { Id = 1, NumberOfTickets=50});

            Assert.AreEqual(50, dbContext.Events.Find(1).NumberOfTickets);

        }
    }
}
