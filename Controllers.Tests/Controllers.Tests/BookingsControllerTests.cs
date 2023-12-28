using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Controllers;
using BookMyShow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BookMyShow.Tests.Controllers.Tests
{
    [TestClass]
    public class BookingsControllerTests
    {
        public static List<Booking> bookingList;
        public Mock<IEventBusiness> _mockEventBusiness;
        public Mock<IBookingBusiness> _mockBookingBusiness;

        [TestInitialize]
        public void SetUp()
        {
            _mockEventBusiness = new Mock<IEventBusiness>();
            _mockBookingBusiness = new Mock<IBookingBusiness>();
            bookingList = new List<Booking>
            {
                new Booking
                {
                    EventId=1,
                    NumberOfTickets=1,
                }
            };
        }
        private ClaimsPrincipal ClaimsForAdmin()
        {
            var userClaims = new Claim[] { new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin") };
            var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));
            return user;
        }

        private ClaimsPrincipal ClaimsForCustomer()
        {
            var userClaims = new Claim[] { new Claim(ClaimTypes.NameIdentifier, "123456"), new Claim(ClaimsIdentity.DefaultRoleClaimType, "Customer") };
            var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));
            return user;
        }

        [TestMethod]
        public void PostBooking_InputBooking_Returns400BadRequestModelError()
        {
            var booking = bookingList[0];
            var user = ClaimsForCustomer();
            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };
            bookingsController.ModelState.AddModelError("NumberOfTickets", "Missing Tickets");

            var response = bookingsController.Post(booking) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);

        }

        [TestMethod]
        public void GetBooking_InputBookingIdbyAdmin_Return200Ok()
        {
            var user = ClaimsForAdmin();
            _mockBookingBusiness.Setup(b => b.GetBooking(It.IsAny<int>(), It.IsAny<string>())).Returns(bookingList[0]);

            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void GetBooking_InputBookingIdbyAdmin_Return404NotFound()
        {
            var user = ClaimsForAdmin();
            _mockBookingBusiness.Setup(b => b.GetBooking(It.IsAny<int>(), It.IsAny<string>())).Returns<Booking>(null);

            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetBooking_InputBookingIdbyAdmin_Return500InternalServerError()
        {
            var user = ClaimsForAdmin();
            _mockBookingBusiness.Setup(b => b.GetBooking(It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception());

            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }


        [TestMethod]
        public void GetAllBookings_GetRequestByAdmin_Return200Ok()
        {
            var user = ClaimsForAdmin();
            _mockBookingBusiness.Setup(b => b.GetAllBookings(It.IsAny<string>())).Returns(bookingList);

            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void GetAllBookings_GetRequestByAdmin_Return404NotFound()
        {
            var user = ClaimsForAdmin();
            _mockBookingBusiness.Setup(b => b.GetAllBookings(It.IsAny<string>())).Returns<List<Booking>>(null);

            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetAllBookings_GetRequestByAdmin_Return500InternalServerError()
        {
            var user = ClaimsForAdmin();
            _mockBookingBusiness.Setup(b => b.GetAllBookings(It.IsAny<string>())).Throws(new Exception());

            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void GetBooking_InputBookingIdByCustomer_Return200Ok()
        {
            var user = ClaimsForCustomer();
            _mockBookingBusiness.Setup(b => b.GetBooking(It.IsAny<int>(), It.IsAny<string>())).Returns(bookingList[0]);

            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void GetBooking_InputBookingIdByCustomer_Return404NotFound()
        {
            var user = ClaimsForCustomer();
            _mockBookingBusiness.Setup(b => b.GetBooking(It.IsAny<int>(), It.IsAny<string>())).Returns<Booking>(null);

            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetBooking_InputBookingIdByCustomer_Return500InternalServerError()
        {
            var user = ClaimsForCustomer();
            _mockBookingBusiness.Setup(b => b.GetBooking(It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception());

            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }


        [TestMethod]
        public void GetAllBookings_GetRequestByCustomer_Return200Ok()
        {
            var user = ClaimsForCustomer();
            _mockBookingBusiness.Setup(b => b.GetAllBookings(It.IsAny<string>())).Returns(bookingList);

            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void GetAllBookings_GetRequestByCustomer_Return404NotFound()
        {
            var user = ClaimsForCustomer();
            _mockBookingBusiness.Setup(b => b.GetAllBookings(It.IsAny<string>())).Returns<List<Booking>>(null);

            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetAllBookings_GetRequestByCustomer_Return500InternalServerError()
        {
            var user = ClaimsForCustomer();
            _mockBookingBusiness.Setup(b => b.GetAllBookings(It.IsAny<string>())).Throws(new Exception());

            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void PostBooking_InputBooking_Return404NotFoundEvent()
        {
            var user = ClaimsForCustomer();
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns<Event>(null);

            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Post(bookingList[0]) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void PostBooking_InputBooking_Return400BadRequest()
        {
            var user = ClaimsForCustomer();
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(new Event());
            _mockBookingBusiness.Setup(b => b.CreateBooking(It.IsAny<Booking>(), It.IsAny<Event>())).Returns(false);
            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Post(bookingList[0]) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void PostBooking_InputBooking_Return200Ok()
        {
            var user = ClaimsForCustomer();
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(new Event());
            _mockBookingBusiness.Setup(b => b.CreateBooking(It.IsAny<Booking>(), It.IsAny<Event>())).Returns(true);
            _mockEventBusiness.Setup(e => e.DecrementTicket(It.IsAny<int>(), It.IsAny<int>()));
            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Post(bookingList[0]) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void PostBooking_InputBooking_Return500InternalServerError()
        {
            var user = ClaimsForCustomer();
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception());
            _mockBookingBusiness.Setup(b => b.CreateBooking(It.IsAny<Booking>(), It.IsAny<Event>())).Returns(true);
            _mockEventBusiness.Setup(e => e.DecrementTicket(It.IsAny<int>(), It.IsAny<int>()));
            var bookingsController = new BookingsController(_mockBookingBusiness.Object, _mockEventBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = bookingsController.Post(bookingList[0]) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }
    }
}
