/*using BookMyShow.Models.Enum;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using BookMyShow.Business;

namespace BookMyShow.Tests.Business.Tests
{
    [TestClass]
    public class BookingBusinessTests
    {
        private Mock<IBookingRepository> _mockBookingRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private List<Booking> bookingList;
        private List<User> userList;

        [TestInitialize]
        public void SetUp()
        {
            _mockBookingRepository = new Mock<IBookingRepository>();
            _mockUserRepository= new Mock<IUserRepository>();
            bookingList = new List<Booking>()
            {
               new Booking(){Id=1,UserId="1234"},
               new Booking(){Id=2,UserId="4321"}
            };
            userList = new List<User>()
            {
                new User(){IdentityUserId="1",Role=Role.Admin},
                new User(){IdentityUserId="2",Role=Role.Organizer},
                new User(){IdentityUserId = "1234", Role=Role.Customer},
                new User(){IdentityUserId = "4", Role=Role.Customer}
            };

        }

        [TestMethod]
        public void GetAllBookings_GetRequestByAdmin_ReturnBookingsList()
        {
            _mockBookingRepository.Setup(b=> b.GetAllBookings()).Returns(bookingList);
            var bookingBusiness= new BookingBusiness(_mockBookingRepository.Object, _mockUserRepository.Object);

            var result = bookingBusiness.GetAllBookings();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetAllBookings_GetRequestByCustomer_ReturnBookingsList()
        {
            _mockBookingRepository.Setup(b => b.GetAllBookings()).Returns(bookingList);
            var bookingBusiness = new BookingBusiness(_mockBookingRepository.Object, _mockUserRepository.Object);

            var result = bookingBusiness.GetAllBookings("1234");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetBooking_InputBookingIdByAdmin_ReturnBooking()
        {
            _mockBookingRepository.Setup(b => b.GetAllBookings()).Returns(bookingList);
            var bookingBusiness = new BookingBusiness(_mockBookingRepository.Object, _mockUserRepository.Object);

            var result = bookingBusiness.GetBooking(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Booking));   
        }

        [TestMethod]
        public void GetBooking_InputBookingIdByAdmin_ReturnsNull()
        {
            _mockBookingRepository.Setup(b => b.GetAllBookings()).Returns(bookingList);
            var bookingBusiness = new BookingBusiness(_mockBookingRepository.Object, _mockUserRepository.Object);

            var result = bookingBusiness.GetBooking(3);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetBooking_InputBookingIdByCustomer_ReturnBooking()
        {
            _mockBookingRepository.Setup(b => b.GetAllBookings()).Returns(bookingList);
            _mockUserRepository.Setup(u => u.GetAllUsers()).Returns(userList);
            var bookingBusiness = new BookingBusiness(_mockBookingRepository.Object, _mockUserRepository.Object);

            var result = bookingBusiness.GetBooking(1,"1234");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Booking));
        }

        [TestMethod]
        public void GetBooking_InputBookingIdByCustomer_ReturnsNullForNoUserFound()
        {
            _mockBookingRepository.Setup(b => b.GetAllBookings()).Returns(bookingList);
            _mockUserRepository.Setup(u => u.GetAllUsers()).Returns(userList);
            var bookingBusiness = new BookingBusiness(_mockBookingRepository.Object, _mockUserRepository.Object);

            var result = bookingBusiness.GetBooking(1, "123");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetBooking_InputBookingIdByCustomer_ReturnsNullForNotSameBooking()
        {
            _mockBookingRepository.Setup(b => b.GetAllBookings()).Returns(bookingList);
            _mockUserRepository.Setup(u => u.GetAllUsers()).Returns(userList);
            var bookingBusiness = new BookingBusiness(_mockBookingRepository.Object, _mockUserRepository.Object);

            var result = bookingBusiness.GetBooking(1, "4");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetBooking_InputBookingIdByCustomer_ReturnsNullForNoBookingFound()
        {
            _mockBookingRepository.Setup(b => b.GetAllBookings()).Returns(bookingList);
            _mockUserRepository.Setup(u => u.GetAllUsers()).Returns(userList);
            var bookingBusiness = new BookingBusiness(_mockBookingRepository.Object, _mockUserRepository.Object);

            var result = bookingBusiness.GetBooking(3, "4");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateBooking_InputBooking_ReturnsTrue()
        {
            var booking = new Booking() { Id = 1, NumberOfTickets = 1 };
            var e = new Event() { NumberOfTickets = 2 };
            _mockBookingRepository.Setup(b => b.AddBooking(It.IsAny<Booking>()));
            var bookingBusiness = new BookingBusiness(_mockBookingRepository.Object,_mockUserRepository.Object);

            var result = bookingBusiness.CreateBooking(booking,e);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateBooking_InputBooking_ReturnsFalseForInvalidTickets()
        {
            var booking = new Booking() { Id = 1, NumberOfTickets = 0 };
            var e = new Event() { NumberOfTickets = 2 };
            _mockBookingRepository.Setup(b => b.AddBooking(It.IsAny<Booking>()));
            var bookingBusiness = new BookingBusiness(_mockBookingRepository.Object, _mockUserRepository.Object);

            var result = bookingBusiness.CreateBooking(booking, e);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateBooking_InputBooking_ReturnsFalseForGreaterNumberOfTickets()
        {
            var booking = new Booking() { Id = 1, NumberOfTickets = 3 };
            var e = new Event() { NumberOfTickets = 2 };
            _mockBookingRepository.Setup(b => b.AddBooking(It.IsAny<Booking>()));
            var bookingBusiness = new BookingBusiness(_mockBookingRepository.Object, _mockUserRepository.Object);

            var result = bookingBusiness.CreateBooking(booking, e);

            Assert.IsFalse(result);
        }
    }
}
*/