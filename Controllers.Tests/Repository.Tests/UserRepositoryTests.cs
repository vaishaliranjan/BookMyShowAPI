/*using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace BookMyShow.Tests.Repository.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private Mock<AppDbContext> _mockDbContext;
        private DbContextOptions<AppDbContext> options;
        private AppDbContext dbContext;
        private UserRepository userRepository;
        [TestInitialize]
        public void SetUp()
        {
            _mockDbContext = new Mock<AppDbContext>();
            options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "InMemoryDatabase").Options;
            dbContext = new AppDbContext(options);
            userRepository = new UserRepository(dbContext);
            dbContext.Users.Add(new User() { IdentityUserId= "1" });
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void GetAllUsers_GetRequest_ReturnsUserList()
        {

            var result = userRepository.GetAllUsers();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void AddUser_InputUser()
        {
            userRepository.AddUser(new User() { IdentityUserId="2"});

            Assert.AreEqual(2,dbContext.Users.Count());

        }

        [TestMethod]
        public void RemoveUser_InputUser_FoundUser()
        {
            userRepository.RemoveUser(new User() { IdentityUserId = "1" });

            Assert.AreEqual(0, dbContext.Users.Count());

        }
    }
}
*/