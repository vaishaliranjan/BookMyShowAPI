/*using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models.Enum;
using BookMyShow.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using BookMyShow.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BookMyShow.Tests.Controllers.Tests
{
    [TestClass]
    public class EventsControllersTests
    {
        public static List<Event> eventList;
        public Mock<IEventBusiness> _mockEventBusiness;
        public Mock<IArtistBusiness> _mockArtistBusiness;
        public Mock<IVenueBusiness> _mockVenueBusiness;
        public Mock<IOrganizerBusiness> _mockOrganizerBusiness;

        [TestInitialize]
        public void SetUp()
        {
            _mockEventBusiness = new Mock<IEventBusiness>();
            _mockArtistBusiness = new Mock<IArtistBusiness>();
            _mockVenueBusiness = new Mock<IVenueBusiness>();
            _mockOrganizerBusiness = new Mock<IOrganizerBusiness>();
            eventList = new List<Event>()
            {
                new Event()
                {
                    Id = 1,
                    EventName = "Test",
                    ArtistId=1,
                    VenueId=1,
                    InitialTickets=100,
                    NumberOfTickets=100,
                    Price=100,
                    UserId="123456"
                }
            };
        }

        private ClaimsPrincipal ClaimsForAdminCustomer(string role)
        {
            var userClaims = new Claim[] { new Claim(ClaimsIdentity.DefaultRoleClaimType, role) };
            var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));
            return user;
        }

        private ClaimsPrincipal ClaimsForOrganizer()
        {
            var userClaims = new Claim[] { new Claim(ClaimTypes.NameIdentifier, "123456"), new Claim(ClaimsIdentity.DefaultRoleClaimType, "Organizer") };
            var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));
            return user;
        }

        [TestMethod]
        public void PostEvent_InputEvent_Returns400BadRequestModelError()
        {
            var e = eventList[0];
            var user = ClaimsForOrganizer();
            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object);
            eventsController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            eventsController.ModelState.AddModelError("Event Name", "Missing Name");

            var response = eventsController.Post(e) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);

        }


        [TestMethod]
        public void GetEvent_InputEventIdbyAdmin_Return200Ok()
        {
            var user = ClaimsForAdminCustomer("Admin");
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(eventList[0]);

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object);
            eventsController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            var response = eventsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }



        [TestMethod]
        public void GetEvent_InputEventIdByAdmin_Return404NotFound()
        {
            var user = ClaimsForAdminCustomer("Admin");
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns<Event>(null);

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object);
            eventsController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var response = eventsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }


        [TestMethod]
        public void GetAllEvents_GetEventsByAdmin_Return200Ok()
        {
            var user = ClaimsForAdminCustomer("Admin");
            _mockEventBusiness.Setup(e => e.GetAllEvents(It.IsAny<string>())).Returns(eventList);

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }


        [TestMethod]
        public void GetAllEvents_GetEventsByAdmin_Return404NotFound()
        {
            var user = ClaimsForAdminCustomer("Admin");
            _mockEventBusiness.Setup(e => e.GetAllEvents(It.IsAny<string>())).Returns<List<Event>>(null);

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }


        [TestMethod]
        public void GetAllEvents_GetEventsByAdmin_Return500InternalServerError()
        {
            var user = ClaimsForAdminCustomer("Admin");

            _mockEventBusiness.Setup(e => e.GetAllEvents(It.IsAny<string>())).Throws(new Exception());

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void GetEvent_InputEventIdByAdmin_Return500InternalServerError()
        {
            var user = ClaimsForAdminCustomer("Admin");
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception());

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void GetEvent_InputEventIdByCustomer_Return200Ok()
        {
            var user = ClaimsForAdminCustomer("Customer");
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(eventList[0]);

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object);
            eventsController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            var response = eventsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }


        [TestMethod]
        public void GetEvent_InputEventIdByCustomer_Return404NotFound()
        {
            var user = ClaimsForAdminCustomer("Customer");
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns<Event>(null);

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object);
            eventsController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var response = eventsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }


        [TestMethod]
        public void GetAllEvents_GetEventsByCustomer_Return200Ok()
        {
            var user = ClaimsForAdminCustomer("Customer");
            _mockEventBusiness.Setup(e => e.GetAllEvents(It.IsAny<string>())).Returns(eventList);

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }


        [TestMethod]
        public void GetAllEvents_GetEventsByCustomer_Return404NotFound()
        {
            var user = ClaimsForAdminCustomer("Customer");
            _mockEventBusiness.Setup(e => e.GetAllEvents(It.IsAny<string>())).Returns<List<Event>>(null);

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }


        [TestMethod]
        public void GetAllEvents_GetEventsByCustomer_Return500InternalServerError()
        {
            var user = ClaimsForAdminCustomer("Customer");

            _mockEventBusiness.Setup(e => e.GetAllEvents(It.IsAny<string>())).Throws(new Exception());

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void GetEvent_InputEventIdByCustomer_Return500InternalServerError()
        {
            var user = ClaimsForAdminCustomer("Customer");
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception());

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void GetEvent_InputEventIdByOrganizer_Return200Ok()
        {
            var user = ClaimsForOrganizer();
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(eventList[0]);

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object);
            eventsController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            var response = eventsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }


        [TestMethod]
        public void GetEvent_InputEventIdByOrganizer_Return404NotFound()
        {
            var user = ClaimsForOrganizer();
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns<Event>(null);

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object);
            eventsController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var response = eventsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }


        [TestMethod]
        public void GetAllEvents_GetEventsByOrganizer_Return200Ok()
        {
            var user = ClaimsForOrganizer();
            _mockEventBusiness.Setup(e => e.GetAllEvents(It.IsAny<string>())).Returns(eventList);

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }


        [TestMethod]
        public void GetAllEvents_GetEventsByOrganizer_Return404NotFound()
        {
            var user = ClaimsForOrganizer();
            _mockEventBusiness.Setup(e => e.GetAllEvents(It.IsAny<string>())).Returns<List<Event>>(null);

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }


        [TestMethod]
        public void GetAllEvents_GetEventsByOrganizer_Return500InternalServerError()
        {
            var user = ClaimsForOrganizer();

            _mockEventBusiness.Setup(e => e.GetAllEvents(It.IsAny<string>())).Throws(new Exception());

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Get(null) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void GetEvent_InputEventIdByOrganizer_Return500InternalServerError()
        {
            var user = ClaimsForOrganizer();
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception());

            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Get(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }


        [TestMethod]
        public void PostEvent_InputEvent_Returns2000Ok()
        {
            var newEvent = eventList[0];
            var user = ClaimsForOrganizer();
            _mockArtistBusiness.Setup(a => a.GetArtist(1)).Returns(new Artist());
            _mockVenueBusiness.Setup(v => v.GetVenue(1)).Returns(new Venue());
            _mockEventBusiness.Setup(e => e.CreateEvent(newEvent));



            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };


            var response = eventsController.Post(newEvent) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }


        [TestMethod]
        public void PostEvent_InputEvent_ReturnsBadRequestForArtist()
        {
            var newEvent = eventList[0];
            var user = ClaimsForOrganizer();
            _mockArtistBusiness.Setup(a => a.GetArtist(1)).Returns<Artist>(null);
            _mockVenueBusiness.Setup(v => v.GetVenue(1)).Returns(new Venue());
            _mockEventBusiness.Setup(e => e.CreateEvent(newEvent));
            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };


            var response = eventsController.Post(newEvent) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void PostEvent_InputEvent_ReturnsBadRequestForVenue()
        {
            var newEvent = eventList[0];
            var user = ClaimsForOrganizer();
            _mockArtistBusiness.Setup(a => a.GetArtist(1)).Returns(new Artist());
            _mockVenueBusiness.Setup(v => v.GetVenue(1)).Returns<Venue>(null);
            _mockEventBusiness.Setup(e => e.CreateEvent(newEvent));
            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };


            var response = eventsController.Post(newEvent) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void PostEvent_InputEvent_ReturnsBadRequestForVenueArtist()
        {
            var newEvent = eventList[0];
            var user = ClaimsForOrganizer();
            _mockArtistBusiness.Setup(a => a.GetArtist(1)).Returns<Artist>(null);
            _mockVenueBusiness.Setup(v => v.GetVenue(1)).Returns<Venue>(null);
            _mockEventBusiness.Setup(e => e.CreateEvent(newEvent));
            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Post(newEvent) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }


        [TestMethod]
        public void PostEvent_InputEvent_Returns5000InternalServerError()
        {
            var newEvent = eventList[0];
            var user = ClaimsForOrganizer();
            _mockArtistBusiness.Setup(a => a.GetArtist(1)).Throws(new Exception());
            _mockVenueBusiness.Setup(v => v.GetVenue(1)).Throws(new Exception());
            _mockEventBusiness.Setup(e => e.CreateEvent(newEvent)).Throws(new Exception());
            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };


            var response = eventsController.Post(newEvent) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void DeleteEvent_InputEventIdByAdmin_Returns200Ok()
        {
            var user = ClaimsForAdminCustomer("Admin");
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(eventList[0]);
            _mockEventBusiness.Setup(e => e.DeleteEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            _mockArtistBusiness.Setup(a => a.UnBookArtist(It.IsAny<int>()));
            _mockVenueBusiness.Setup(v => v.UnBookVenue(It.IsAny<int>()));
            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Delete(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }


        [TestMethod]
        public void DeleteEvent_InputEventIdByAdmin_Returns404NotFoundk()
        {
            var user = ClaimsForAdminCustomer("Admin");
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(eventList[0]);
            _mockEventBusiness.Setup(e => e.DeleteEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(false);
            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Delete(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }


        [TestMethod]
        public void DeleteEvent_InputEventIdByAdmin_Returns500InternalServerError()
        {
            var user = ClaimsForAdminCustomer("Admin");
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(eventList[0]);
            _mockEventBusiness.Setup(e => e.DeleteEvent(It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception());
            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Delete(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void DeleteEvent_InputEventIdByOrganizer_Returns200Ok()
        {
            var user = ClaimsForOrganizer();
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(eventList[0]);
            _mockEventBusiness.Setup(e => e.DeleteEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            _mockArtistBusiness.Setup(a => a.UnBookArtist(It.IsAny<int>()));
            _mockVenueBusiness.Setup(v => v.UnBookVenue(It.IsAny<int>()));
            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Delete(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void DeleteEvent_InputEventIdByOrganizer_Returns404NotFoundk()
        {
            var user = ClaimsForOrganizer();
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(eventList[0]);
            _mockEventBusiness.Setup(e => e.DeleteEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(false);
            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Delete(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }


        [TestMethod]
        public void DeleteEvent_InputEventIdByOrganizer_Returns500InternalServerError()
        {
            var user = ClaimsForOrganizer();
            _mockEventBusiness.Setup(e => e.GetEvent(It.IsAny<int>(), It.IsAny<string>())).Returns(eventList[0]);
            _mockEventBusiness.Setup(e => e.DeleteEvent(It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception());
            var eventsController = new EventsController(_mockEventBusiness.Object, _mockArtistBusiness.Object, _mockVenueBusiness.Object, _mockOrganizerBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = eventsController.Delete(1) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }
    }
}
*/