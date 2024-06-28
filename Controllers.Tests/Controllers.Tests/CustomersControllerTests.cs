/*using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Controllers;
using BookMyShow.Models;
using BookMyShow.Models.Enum;
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
    public class CustomersControllerTests
    {
        public static List<User> userList;
        public Mock<ICustomerBusiness> _mockCustomerBusiness;

        [TestInitialize]
        public void SetUp()
        {
            _mockCustomerBusiness = new Mock<ICustomerBusiness>();
            userList = new List<User>()
            {
                new User()
                {
                    Name="Vaishali",
                    Username= "vaishali",
                    IdentityUserId="123456",
                    Role=Role.Customer
                },
                new User()
                {
                    Name="Ritika",
                    Username= "ritika",
                    IdentityUserId="1234676",
                    Role=Role.Customer
                }
            };
        }

        [TestMethod]
        public void GetCustomerDetails_InputCustomerId_Return200Ok()
        {
            var customerId = "123456";
            _mockCustomerBusiness.Setup(c => c.GetCustomer(customerId)).Returns(userList[0]);
            var customersController = new CustomersController(_mockCustomerBusiness.Object);

            var response = customersController.Get(customerId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void GetCustomerDetails_InputCustomerId_Return404NotFound()
        {
            string customerId = "123456";
            User customer = null;
            _mockCustomerBusiness.Setup(c => c.GetCustomer(customerId)).Returns(customer);
            var customersController = new CustomersController(_mockCustomerBusiness.Object);

            var response = customersController.Get(customerId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetCustomerDetails_InputCustomerId_Return500InternalServerError()
        {
            string customerId = "123456";
            _mockCustomerBusiness.Setup(c => c.GetCustomer(customerId)).Throws(new Exception());
            var customersController = new CustomersController(_mockCustomerBusiness.Object);

            var response = customersController.Get(customerId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void GetAllCustomerDetails_GetRequest_Return200Ok()
        {
            string customerId = null;
            _mockCustomerBusiness.Setup(c => c.GetAllCustomers()).Returns(userList);
            var customersController = new CustomersController(_mockCustomerBusiness.Object);

            var response = customersController.Get(customerId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [TestMethod]
        public void GetAllCustomerDetails_GetRequest_Return404NotFound()
        {
            string customerId = null;
            _mockCustomerBusiness.Setup(c => c.GetAllCustomers()).Returns<List<User>>(null);
            var customersController = new CustomersController(_mockCustomerBusiness.Object);

            var response = customersController.Get(customerId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetAllCustomerDetails_GetRequest_Return500InternalServerError()
        {
            string customerId = null;
            _mockCustomerBusiness.Setup(c => c.GetAllCustomers()).Throws(new Exception());
            var customersController = new CustomersController(_mockCustomerBusiness.Object);

            var response = customersController.Get(customerId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
        }
    }
}
*/