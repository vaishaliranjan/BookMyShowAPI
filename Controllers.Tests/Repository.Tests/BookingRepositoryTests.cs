/*using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace BookMyShow.Tests.Repository.Tests
{
    [TestClass]
    public class BookingRepositoryTests
    {
        private Mock<AppDbContext> _mockDbContext;
        private DbContextOptions<AppDbContext> options;
        private AppDbContext dbContext;
        private BookingRepository bookingRepository;
        [TestInitialize]
        public void SetUp()
        {
            _mockDbContext = new Mock<AppDbContext>();
            options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "InMemoryDatabase").Options;
            dbContext = new AppDbContext(options);
            bookingRepository= new BookingRepository(dbContext);    
            dbContext.Bookings.Add(new Booking() { Id=1,NumberOfTickets=100});
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void GetAllBookings_GetRequest_ReturnsBookingList()
        {

            var result = bookingRepository.GetAllBookings();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void AddBooking_InputBooking()
        {
            bookingRepository.AddBooking(new Booking());

            Assert.AreEqual(2, dbContext.Bookings.Count());

        }
  
    }
}
*/