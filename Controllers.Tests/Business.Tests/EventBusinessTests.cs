using BookMyShow.Models.Enum;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using BookMyShow.Business;

namespace BookMyShow.Tests.Business.Tests
{
    [TestClass]
    public class EventBusinessTests
    {
        private Mock<IEventRepository> _mockEventRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private List<Event> eventList;
        private List<User> userList;

        [TestInitialize]
        public void SetUp()
        {
            _mockEventRepository = new Mock<IEventRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            eventList = new List<Event>()
            {
               new Event() { Id = 1, UserId="1234",InitialTickets=5, NumberOfTickets=5},
               new Event() { Id = 2, UserId="4321", InitialTickets=3,NumberOfTickets=3},
               new Event() { Id = 3, UserId="1234", InitialTickets=3,NumberOfTickets=1}
            };
            userList = new List<User>()
            {
                new User(){IdentityUserId="1",Role=Role.Admin},
                new User(){IdentityUserId="2",Role=Role.Organizer},
                new User(){IdentityUserId = "1234", Role=Role.Organizer},
                new User(){IdentityUserId = "4", Role=Role.Organizer}
            };

        }

        [TestMethod]
        public void GetAllEvents_GetRequestByAdmin_ReturnEventList()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.GetAllEvents();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void GetAllEvent_GetRequestByOrganizer_ReturnEventList()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.GetAllEvents("1234");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetEvent_InputEventIdByAdmin_ReturnEvent()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.GetEvent(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Event));
        }

        [TestMethod]
        public void GetEvent_InputEventIdByAdmin_ReturnNull()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.GetEvent(4);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetEvent_InputEventIdByOrganizer_ReturnsEvent()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            _mockUserRepository.Setup(u => u.GetAllUsers()).Returns(userList);
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.GetEvent(1, "1234");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Event));
        }

        [TestMethod]
        public void GetEvent_InputEventIdByOrganizer_ReturnsNullForNoUserFound()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            _mockUserRepository.Setup(u => u.GetAllUsers()).Returns(userList);
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.GetEvent(1, "123");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetEvent_InputEventIdByOrganizer_ReturnsNullForNotSameBooking()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            _mockUserRepository.Setup(u => u.GetAllUsers()).Returns(userList);
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.GetEvent(1, "2");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetEvent_InputEventIdByOrganizer_ReturnsNullForNoEventFound()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            _mockUserRepository.Setup(u => u.GetAllUsers()).Returns(userList);
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.GetEvent(4, "2");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateEvent_InputEvent()
        {

            _mockEventRepository.Setup(e => e.AddEvent(It.IsAny<Event>()));
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            eventBusiness.CreateEvent(eventList[0]);

            Assert.IsInstanceOfType(eventList[0], typeof(Event));

        }

        [TestMethod]
        public void DecrementTicketInAnEvent_InputEventIdAndNumberOfTicketsBought_ReturnsTrue()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            _mockEventRepository.Setup(e => e.UpdateEvent(It.IsAny<Event>()));
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.DecrementTicket(1,2);
            Assert.IsTrue(result);
            Assert.AreEqual(eventList[0].NumberOfTickets, 3);

        }

        [TestMethod]
        public void DecrementTicketInAnEvent_InputEventIdAndNumberOfTicketsBought_ReturnsFalse()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            _mockEventRepository.Setup(e => e.UpdateEvent(It.IsAny<Event>()));
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.DecrementTicket(5, 2);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void DeleteEvent_InputEventIdByAdmin_ReturnTrue()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            _mockEventRepository.Setup(e => e.RemoveEvent(It.IsAny<Event>()));
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result= eventBusiness.DeleteEvent(1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteEvent_InputEventIdByAdmin_ReturnFalseForEventNotFound()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            _mockEventRepository.Setup(e => e.RemoveEvent(It.IsAny<Event>()));
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.DeleteEvent(4);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteEvent_InputEventIdByAdmin_ReturnInitialNumberOfTicketsAreNotSame()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            _mockEventRepository.Setup(e => e.RemoveEvent(It.IsAny<Event>()));
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.DeleteEvent(3);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteEvent_InputEventIdByOrganizer_ReturnTrue()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            _mockEventRepository.Setup(e => e.RemoveEvent(It.IsAny<Event>()));
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.DeleteEvent(1,"1234");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteEvent_InputEventIdByOrganizer_ReturnFalseForEventNotFound()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            _mockEventRepository.Setup(e => e.RemoveEvent(It.IsAny<Event>()));
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.DeleteEvent(4,"1234");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteEvent_InputEventIdByOrganizer_ReturnInitialNumberOfTicketsAreNotSame()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            _mockEventRepository.Setup(e => e.RemoveEvent(It.IsAny<Event>()));
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.DeleteEvent(3,"1234");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteEvent_InputEventIdByOrganizer_ReturnFalseThatNotSameUser()
        {
            _mockEventRepository.Setup(e => e.GetAllEvents()).Returns(eventList);
            _mockEventRepository.Setup(e => e.RemoveEvent(It.IsAny<Event>()));
            var eventBusiness = new EventBusiness(_mockEventRepository.Object, _mockUserRepository.Object);

            var result = eventBusiness.DeleteEvent(3, "2");

            Assert.IsFalse(result);
        }
    }
}
