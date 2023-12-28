using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using BookMyShow.Business;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BookMyShow.Models.ViewsModel;

namespace BookMyShow.Tests.Business.Tests
{
    [TestClass]
    public class AuthenticationBusinessTests
    {
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private Mock<SignInManager<IdentityUser>> _mockSignInManager;
        private Mock<IUserRepository> _mockUserRepository;

        [TestInitialize]
        public void SetUp()
        {
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                  new Mock<IUserStore<IdentityUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<IdentityUser>>().Object,
                  new IUserValidator<IdentityUser>[0],
                  new IPasswordValidator<IdentityUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<IdentityUser>>>().Object
              );

            _mockSignInManager = new Mock<SignInManager<IdentityUser>>(
                _mockUserManager.Object,
                 new Mock<IHttpContextAccessor>().Object,
                 new Mock<IUserClaimsPrincipalFactory<IdentityUser>>().Object,
                 new Mock<IOptions<IdentityOptions>>().Object,
                 new Mock<ILogger<SignInManager<IdentityUser>>>().Object,
                 new Mock<IAuthenticationSchemeProvider>().Object,
                 new Mock<IUserConfirmation<IdentityUser>>().Object);
            _mockUserRepository = new Mock<IUserRepository>();
        }



        [TestMethod]
        public async Task LogOut_RequestLogOut()
        {
            _mockSignInManager.Setup(s => s.SignOutAsync());
            var authenticationBusiness = new AuthenticationBusiness(_mockUserManager.Object,_mockSignInManager.Object,_mockUserRepository.Object);

            await authenticationBusiness.Logout();
        }

        [TestMethod]
        public async Task Login_InputCredentials_ReturnsTrue()
        {
            var login = new LoginModel() { Username = "vaishali", Password = "Vaishali@12345" };
            _mockSignInManager.Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(Task.FromResult(SignInResult.Success));
            var authenticationBusiness = new AuthenticationBusiness(_mockUserManager.Object, _mockSignInManager.Object, _mockUserRepository.Object);

            var result = await authenticationBusiness.Login(login);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Login_InputCredentials_ReturnsFalse()
        {
            var login = new LoginModel() { Username = "vaishali", Password = "Vaishali@12345" };
            _mockSignInManager.Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(Task.FromResult(SignInResult.Failed));
            var authenticationBusiness = new AuthenticationBusiness(_mockUserManager.Object, _mockSignInManager.Object, _mockUserRepository.Object);

            var result = await authenticationBusiness.Login(login);

            Assert.IsFalse(result);
        }


        [TestMethod]
        public async Task Register_InputDetails_ReturnsTrue()
        {
            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));
            _mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()));
            var authenticationBusiness = new AuthenticationBusiness(_mockUserManager.Object, _mockSignInManager.Object, _mockUserRepository.Object);

            var result = await authenticationBusiness.Register(new RegisterModel());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Register_InputDetails_ReturnsFalse()
        {
            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError())));
            _mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()));
            var authenticationBusiness = new AuthenticationBusiness(_mockUserManager.Object, _mockSignInManager.Object, _mockUserRepository.Object);

            var result = await authenticationBusiness.Register(new RegisterModel());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task AddAdmin_InputDetailsByAdmin_ReturnsTrue()
        {
            var user = new AddUserModel() { RoleId = "1" };
            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));
            _mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()));
            _mockUserRepository.Setup(u => u.AddUser(It.IsAny<User>()));
            var authenticationBusiness = new AuthenticationBusiness(_mockUserManager.Object, _mockSignInManager.Object, _mockUserRepository.Object);

            var result = await authenticationBusiness.AddUser(user);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task AddAdmin_InputDetailsByAdmin_ReturnsFalse()
        {
            var user = new AddUserModel() { RoleId = "1" };
            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError())));
            _mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()));
            var authenticationBusiness = new AuthenticationBusiness(_mockUserManager.Object, _mockSignInManager.Object, _mockUserRepository.Object);

            var result = await authenticationBusiness.AddUser(user);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task AddOrganizer_InputDetailsByAdmin_ReturnsTrue()
        {
            var user = new AddUserModel() { RoleId = "2" };
            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));
            _mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()));
            _mockUserRepository.Setup(u => u.AddUser(It.IsAny<User>()));
            var authenticationBusiness = new AuthenticationBusiness(_mockUserManager.Object, _mockSignInManager.Object, _mockUserRepository.Object);

            var result = await authenticationBusiness.AddUser(user);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task AddOrganizer_InputDetailsByAdmin_ReturnsFalse()
        {
            var user = new AddUserModel() { RoleId = "2" };
            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError())));
            _mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()));
            var authenticationBusiness = new AuthenticationBusiness(_mockUserManager.Object, _mockSignInManager.Object, _mockUserRepository.Object);

            var result = await authenticationBusiness.AddUser(user);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task AddCustomer_InputDetailsByAdmin_ReturnsTrue()
        {
            var user = new AddUserModel() { RoleId = "3" };
            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));
            _mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()));
            _mockUserRepository.Setup(u => u.AddUser(It.IsAny<User>()));
            var authenticationBusiness = new AuthenticationBusiness(_mockUserManager.Object, _mockSignInManager.Object, _mockUserRepository.Object);

            var result = await authenticationBusiness.AddUser(user);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task AddCustomer_InputDetailsByAdmin_ReturnsFalse()
        {
            var user = new AddUserModel() { RoleId = "3" };
            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError())));
            _mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()));
            var authenticationBusiness = new AuthenticationBusiness(_mockUserManager.Object, _mockSignInManager.Object, _mockUserRepository.Object);

            var result = await authenticationBusiness.AddUser(user);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task AddUser_InputInvalidRoleDetailsByAdmin_ReturnsFalse()
        {
            var user = new AddUserModel() { RoleId = "4" };
            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError())));
            _mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()));
            var authenticationBusiness = new AuthenticationBusiness(_mockUserManager.Object, _mockSignInManager.Object, _mockUserRepository.Object);

            var result = await authenticationBusiness.AddUser(user);

            Assert.IsFalse(result);
        }
    }
}
