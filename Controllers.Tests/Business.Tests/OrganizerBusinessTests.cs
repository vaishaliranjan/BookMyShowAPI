/*using BookMyShow.Business;
using BookMyShow.Models.Enum;
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
    public class OrganizerBusinessTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private List<User> userList;


        [TestInitialize]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            userList = new List<User>()
            {
                new User(){IdentityUserId="1",Role=Role.Admin},
                new User(){IdentityUserId="2",Role=Role.Organizer},
                new User(){IdentityUserId = "3", Role=Role.Organizer},
                new User(){IdentityUserId = "4", Role=Role.Customer}
            };

        }

        [TestMethod]
        public void GetAllOrganizers_GetRequest_ReturnsListOfOrganizers()
        {
            _mockUserRepository.Setup(o => o.GetAllUsers()).Returns(userList);
            var organizerBusiness = new OrganizerBusiness(_mockUserRepository.Object);
            var result = organizerBusiness.GetAllOrganizers();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.TrueForAll(u => u.Role == Role.Organizer));
        }

        [TestMethod]
        public void GetOrganizer_InputOrganizerId_ReturnsOrganizer()
        {
            _mockUserRepository.Setup(o => o.GetAllUsers()).Returns(userList);
            var organizerBusiness = new OrganizerBusiness(_mockUserRepository.Object);
            var result = organizerBusiness.GetOrganizer("2");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(User));
        }

        [TestMethod]
        public void GetOrganizer_InputOrganizerId_ReturnsNull()
        {
            _mockUserRepository.Setup(o => o.GetAllUsers()).Returns(userList);
            var organizerBusiness = new OrganizerBusiness(_mockUserRepository.Object);
            var result = organizerBusiness.GetOrganizer("4");

            Assert.IsNull(result);
        }
    }
}
*/