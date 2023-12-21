using BookMyShow.Business;
using BookMyShow.Models.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookMyShow.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationBusiness _accountBusiness;
        public AuthenticationController(IAuthenticationBusiness accountBusiness)
        {
            _accountBusiness = accountBusiness;
        }


        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Users([FromBody] AddUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _accountBusiness.AddUser(model);
            if (result)
            {
                return Ok("Account created successfully.");
            }
            return StatusCode(StatusCodes.Status400BadRequest, "User already exists");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _accountBusiness.Register(model);
            if (result)
            {
                return Ok("Account created successfully.");
            }
            return StatusCode(StatusCodes.Status400BadRequest,"User already exists");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountBusiness.Login(model);
            if (result)
            {
                return Ok("Logged In successfully.");
            }
            return StatusCode(StatusCodes.Status404NotFound, "User Not Found");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountBusiness.Logout();
            return Ok("Logged out successfully.");
        }
    }
}
