using BookMyShow.Business;
using BookMyShow.Controllers;
using BookMyShow.Models.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookMyShow.Tests.Controllers.Tests
{
    [TestClass]
    public class AuthenticationControllerTests
    {
        private Mock<IAuthenticationBusiness> _mockAuthenticationBusiness;

        [TestInitialize]
        public void ClassInitialize()
        {
            _mockAuthenticationBusiness = new Mock<IAuthenticationBusiness>();
        }

        private ClaimsPrincipal ClaimsForAdmin()
        {
            var userClaims = new Claim[] { new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin") };
            var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));
            return user;
        }

        [TestMethod]
        public async Task LogOut_InputPostRequest_Return200Ok()
        {
            _mockAuthenticationBusiness.Setup(a => a.Logout());
            var authenticationControllers = new AuthenticationController(_mockAuthenticationBusiness.Object);

            var response = await authenticationControllers.Logout() as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);

        }

        [TestMethod]
        public async Task LogOut_InputPostRequest_Return500InternalServerError()
        {
            _mockAuthenticationBusiness.Setup(a => a.Logout()).Throws(new Exception());
            var authenticationControllers = new AuthenticationController(_mockAuthenticationBusiness.Object);

            var response = await authenticationControllers.Logout() as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);

        }


        [TestMethod]
        public async Task Login_InputCredentials_Return200Ok()
        {
            _mockAuthenticationBusiness.Setup(a => a.Login(It.IsAny<LoginModel>())).ReturnsAsync(true);
            var authenticationControllers = new AuthenticationController(_mockAuthenticationBusiness.Object);

            var response = await authenticationControllers.Login(new LoginModel()) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);

        }

        [TestMethod]
        public async Task Login_InputCredentials_Return404NotFound()
        {
            _mockAuthenticationBusiness.Setup(a => a.Login(It.IsAny<LoginModel>())).ReturnsAsync(false);
            var authenticationControllers = new AuthenticationController(_mockAuthenticationBusiness.Object);

            var response = await authenticationControllers.Login(new LoginModel()) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);

        }

        [TestMethod]
        public async Task Login_InputCredentials_Return500InternalServerError()
        {
            _mockAuthenticationBusiness.Setup(a => a.Login(It.IsAny<LoginModel>())).Throws(new Exception());
            var authenticationControllers = new AuthenticationController(_mockAuthenticationBusiness.Object);

            var response = await authenticationControllers.Login(new LoginModel()) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);

        }

        [TestMethod]
        public async Task Register_InputDetails_Return200Ok()
        {
            _mockAuthenticationBusiness.Setup(a => a.Register(It.IsAny<RegisterModel>())).ReturnsAsync(true);
            var authenticationControllers = new AuthenticationController(_mockAuthenticationBusiness.Object);

            var response = await authenticationControllers.Register(new RegisterModel()) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);

        }

        [TestMethod]
        public async Task Register_InputDetails_Return400BadRequestUserAlreadyExist()
        {
            _mockAuthenticationBusiness.Setup(a => a.Register(It.IsAny<RegisterModel>())).ReturnsAsync(false);
            var authenticationControllers = new AuthenticationController(_mockAuthenticationBusiness.Object);

            var response = await authenticationControllers.Register(new RegisterModel()) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);

        }

        [TestMethod]
        public async Task Register_InputDetails_Return500InternalServerError()
        {
            _mockAuthenticationBusiness.Setup(a => a.Register(It.IsAny<RegisterModel>())).Throws(new Exception());
            var authenticationControllers = new AuthenticationController(_mockAuthenticationBusiness.Object);

            var response = await authenticationControllers.Register(new RegisterModel()) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);

        }


        [TestMethod]
        public async Task AddUser_InputDetailsByAdmin_Return200Ok()
        {
            var user = ClaimsForAdmin();
            _mockAuthenticationBusiness.Setup(a => a.AddUser(It.IsAny<AddUserModel>())).ReturnsAsync(true);
            var authenticationControllers = new AuthenticationController(_mockAuthenticationBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = await authenticationControllers.Users(new AddUserModel()) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);

        }

        [TestMethod]
        public async Task AddUser_InputDetailsByAdmin_Return400BadRequestUserAlreadyExist()
        {
            var user = ClaimsForAdmin();
            _mockAuthenticationBusiness.Setup(a => a.AddUser(It.IsAny<AddUserModel>())).ReturnsAsync(false);
            var authenticationControllers = new AuthenticationController(_mockAuthenticationBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = await authenticationControllers.Users(new AddUserModel()) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);

        }

        [TestMethod]
        public async Task AddUser_InputDetailsByAdmin_Return500InternalServerError()
        {
            var user = ClaimsForAdmin();
            _mockAuthenticationBusiness.Setup(a => a.AddUser(It.IsAny<AddUserModel>())).Throws(new Exception());
            var authenticationControllers = new AuthenticationController(_mockAuthenticationBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = await authenticationControllers.Users(new AddUserModel()) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);

        }

        [TestMethod]
        public async Task PostUser_InputUserDetailsByAdmin_Returns400BadRequestModelError()
        {
            var newUser = new AddUserModel();
            var user = ClaimsForAdmin();
            var authenticationControllers = new AuthenticationController(_mockAuthenticationBusiness.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };
            authenticationControllers.ModelState.AddModelError("Name", "Missing Name");

            var response = await authenticationControllers.Users(newUser) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);

        }

        [TestMethod]
        public async Task Register_InputDetails_Returns400BadRequestModelError()
        {
            var authenticationControllers = new AuthenticationController(_mockAuthenticationBusiness.Object);
            authenticationControllers.ModelState.AddModelError("Name", "Missing Name");

            var response = await authenticationControllers.Register(new RegisterModel()) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);

        }

        [TestMethod]
        public async Task Login_InputCredentials_Returns400BadRequestModelError()
        {
            var authenticationControllers = new AuthenticationController(_mockAuthenticationBusiness.Object);
            authenticationControllers.ModelState.AddModelError("Username", "Missing Username");

            var response = await authenticationControllers.Login(new LoginModel()) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);

        }
    }
}
