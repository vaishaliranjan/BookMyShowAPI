/*using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Controllers;
using BookMyShow.Models.Enum;
using BookMyShow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System;
namespace BookMyShow.Tests.Controllers.Tests
{
    [TestClass]
    public class OrganizersControllerTests
    {
        public static List<User> userList;
        public Mock<IOrganizerBusiness> _mockOrganizerBusiness;

        [TestInitialize]
        public void SetUp()
        {
            _mockOrganizerBusiness = new Mock<IOrganizerBusiness>();
            userList = new List<User>()
        {
            new User()
            {
                Name="Vaishali",
                Username= "vaishali",
                IdentityUserId="123456",
                Role=Role.Organizer
            },
            new User()
            {
                Name="Ritika",
                Username= "ritika",
                IdentityUserId="1234676",
                Role=Role.Organizer
            }
        };
        }

        [TestMethod]
        public void GetOrganizerDetails_InputOrganizerId_Return200Ok()
        {
            var organizerId = "123456";
            _mockOrganizerBusiness.Setup(o => o.GetOrganizer(organizerId)).Returns(userList[0]);
            var organizersController = new OrganizersController(_mockOrganizerBusiness.Object);

            var response = organizersController.Get(organizerId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void GetOrganizerDetails_InputOrganizerId_Return404NotFound()
        {
            var organizerId = "123456";
            User organizer = null;
            _mockOrganizerBusiness.Setup(o => o.GetOrganizer(organizerId)).Returns(organizer);
            var organizersController = new OrganizersController(_mockOrganizerBusiness.Object);

            var response = organizersController.Get(organizerId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetOrganizerDetails_InputOrganizerId_Return500InternalServerError()
        {
            var organizerId = "123456";
            _mockOrganizerBusiness.Setup(o => o.GetOrganizer(organizerId)).Throws(new Exception());
            var organizersController = new OrganizersController(_mockOrganizerBusiness.Object);

            var response = organizersController.Get(organizerId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void GetAllOrganizerDetails_GetRequest_Return200Ok()
        {
            string organizerId = null;
            _mockOrganizerBusiness.Setup(o => o.GetAllOrganizers()).Returns(userList);
            var organizersController = new OrganizersController(_mockOrganizerBusiness.Object);

            var response = organizersController.Get(organizerId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void GetAllOrganizerDetails_GetRequest_Return404NotFound()
        {
            string organizerId = null;
            _mockOrganizerBusiness.Setup(o => o.GetAllOrganizers()).Returns<List<User>>(null);
            var organizersController = new OrganizersController(_mockOrganizerBusiness.Object);

            var response = organizersController.Get(organizerId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetAllOrganizerDetails_GetRequest_Return500InternalServerError()
        {
            string organizerId = null;
            _mockOrganizerBusiness.Setup(o => o.GetAllOrganizers()).Throws(new Exception());
            var organizersController = new OrganizersController(_mockOrganizerBusiness.Object);

            var response = organizersController.Get(organizerId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }
    }
}*/