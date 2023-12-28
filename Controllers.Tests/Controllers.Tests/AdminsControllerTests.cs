using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Controllers;
using BookMyShow.Models.Enum;
using BookMyShow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyShow.Tests.Controllers.Tests
{
    [TestClass]
    public class AdminControllerTests
    {
        public static List<User> userList;
        public Mock<IAdminBusiness> _mockAdminBusiness;

        [TestInitialize]
        public void SetUp()
        {
            _mockAdminBusiness = new Mock<IAdminBusiness>();
            userList = new List<User>()
        {
            new User()
            {
                Name="Admin1",
                Username= "admin1",
                IdentityUserId="111111",
                Role=Role.Admin
            },
            new User()
            {
                Name="Admin2",
                Username= "admin2",
                IdentityUserId="222222",
                Role=Role.Admin
            }
            };
        }

        [TestMethod]
        public void GetAdminDetails_InputAdminId_Return200Ok()
        {
            var adminId = "111111";
            _mockAdminBusiness.Setup(a => a.GetAdmin(adminId)).Returns(userList[0]);
            var adminController = new AdminsController(_mockAdminBusiness.Object);

            var response = adminController.Get(adminId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void GetAdminDetails_InputAdminId_Return404NotFound()
        {
            var adminId = "111111";
            User admin = null;
            _mockAdminBusiness.Setup(a => a.GetAdmin(adminId)).Returns(admin);
            var adminController = new AdminsController(_mockAdminBusiness.Object);

            var response = adminController.Get(adminId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetAdminDetails_InputAdminId_Return500InternalServerError()
        {
            var adminId = "111111";
            _mockAdminBusiness.Setup(a => a.GetAdmin(adminId)).Throws(new Exception());
            var adminController = new AdminsController(_mockAdminBusiness.Object);

            var response = adminController.Get(adminId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void GetAllAdminDetails_GetRequest_Return200Ok()
        {
            string adminId = null;
            _mockAdminBusiness.Setup(a => a.GetAllAdmins()).Returns(userList);
            var adminController = new AdminsController(_mockAdminBusiness.Object);

            var response = adminController.Get(adminId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void GetAllAdminDetails_GetRequest_Return404NotFound()
        {
            string adminId = null;
            _mockAdminBusiness.Setup(a => a.GetAllAdmins()).Returns<List<User>>(null);
            var adminController = new AdminsController(_mockAdminBusiness.Object);

            var response = adminController.Get(adminId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetAllAdminDetails_GetRequest_Return500InternalServerError()
        {
            string adminId = null;
            _mockAdminBusiness.Setup(a => a.GetAllAdmins()).Throws(new Exception());
            var adminController = new AdminsController(_mockAdminBusiness.Object);

            var response = adminController.Get(adminId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void DeleteAdmin_InputAdminId_Return200Ok()
        {
            string adminId = "123456";
            _mockAdminBusiness.Setup(a => a.DeleteAdmin(adminId)).Returns(true);
            var adminController = new AdminsController(_mockAdminBusiness.Object);

            var response = adminController.Delete(adminId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void DeleteAdmin_InputAdminId_Return404NotFound()
        {
            string adminId = "123456";
            _mockAdminBusiness.Setup(a => a.DeleteAdmin(adminId)).Returns(false);
            var adminController = new AdminsController(_mockAdminBusiness.Object);

            var response = adminController.Delete(adminId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void DeleteAdmin_InputAdminId_Return500InternalServerError()
        {
            string adminId = "123456";
            _mockAdminBusiness.Setup(a => a.DeleteAdmin(adminId)).Throws(new Exception());
            var adminController = new AdminsController(_mockAdminBusiness.Object);

            var response = adminController.Delete(adminId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }
    }
}
