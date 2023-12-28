using BookMyShow.Business;
using BookMyShow.Models;
using BookMyShow.Models.Enum;
using BookMyShow.Repository.IRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace BookMyShow.Tests.Business.Tests
{
    [TestClass]
    public class CustomerBusinessTests
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
                new User(){IdentityUserId = "3", Role=Role.Customer},
                new User(){IdentityUserId = "4", Role=Role.Customer}
            };

        }

        [TestMethod]
        public void GetAllCustomers_GetRequest_ReturnsListOfCustomer()
        {
            _mockUserRepository.Setup(c => c.GetAllUsers()).Returns(userList);
            var customerBusiness = new CustomerBusiness(_mockUserRepository.Object);
            var result = customerBusiness.GetAllCustomers();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.TrueForAll(u => u.Role == Role.Customer));
        }

        [TestMethod]
        public void GetCustomer_InputCustomerId_ReturnsCustomer()
        {
            _mockUserRepository.Setup(c => c.GetAllUsers()).Returns(userList);
            var customerBusiness = new CustomerBusiness(_mockUserRepository.Object);
            var result = customerBusiness.GetCustomer("3");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(User));
        }

        [TestMethod]
        public void GetCustomer_InputCustomerId_ReturnsNull()
        {
            _mockUserRepository.Setup(c => c.GetAllUsers()).Returns(userList);
            var customerBusiness = new CustomerBusiness(_mockUserRepository.Object);
            var result = customerBusiness.GetCustomer("2");

            Assert.IsNull(result);
        }
    }
}
