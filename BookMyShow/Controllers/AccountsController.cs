using BookMyShow.Models.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
      
        [HttpPost]
        public IActionResult Register([FromBody] RegisterModel input)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var identityUser = new IdentityUser { UserName = input.Username };
        }
    }
}
