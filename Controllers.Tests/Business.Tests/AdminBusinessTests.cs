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
    public class AdminBusinessTests
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
                new User(){IdentityUserId="2",Role=Role.Admin},
                new User(){IdentityUserId = "3", Role=Role.Organizer},
                new User(){IdentityUserId = "4", Role=Role.Customer}
            };

        }

        [TestMethod]
        public void GetAllAdmins_GetRequest_ReturnsListOfAdmin()
        {
            _mockUserRepository.Setup(a => a.GetAllUsers()).Returns(userList);
            var adminBusiness = new AdminBusiness(_mockUserRepository.Object);
            var result = adminBusiness.GetAllAdmins();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.TrueForAll(u => u.Role == Role.Admin));
        }

        [TestMethod]
        public void GetAdmin_InputAdminId_ReturnsAdmin()
        {
            _mockUserRepository.Setup(a => a.GetAllUsers()).Returns(userList);
            var adminBusiness = new AdminBusiness(_mockUserRepository.Object);
            var result = adminBusiness.GetAdmin("1");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(User));
        }

        [TestMethod]
        public void GetAdmin_InputAdminId_ReturnsNull()
        {
            _mockUserRepository.Setup(a => a.GetAllUsers()).Returns(userList);
            var adminBusiness = new AdminBusiness(_mockUserRepository.Object);
            var result = adminBusiness.GetAdmin("3");

            Assert.IsNull(result);
        }


        [TestMethod]
        public void DeleteAdmin_InputAdminId_ReturnsTrue()
        {
            _mockUserRepository.Setup(a => a.GetAllUsers()).Returns(userList);
            var adminBusiness = new AdminBusiness(_mockUserRepository.Object);
            var result = adminBusiness.DeleteAdmin("1");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteAdmin_InputAdminId_ReturnsFalse()
        {
            _mockUserRepository.Setup(a => a.GetAllUsers()).Returns(userList);
            var adminBusiness = new AdminBusiness(_mockUserRepository.Object);
            var result = adminBusiness.DeleteAdmin("3");

            Assert.IsFalse(result);
        }
    }
}
*/