using BookMyShow.Business;
using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private IAdminBusiness _adminBusiness;
        public AdminsController(IAdminBusiness adminBusiness)
        {
            _adminBusiness = adminBusiness;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Get(string id)
        {
            if (id == null)
            {
                return Ok(_adminBusiness.GetAllAdmins());
            }
            var admin = _adminBusiness.GetAdmin(id);
            if (admin == null)
            {
                return NotFound("Admin not found!");
            }
            return Ok(admin);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            var result = _adminBusiness.DeleteAdmin(id);
            if (result)
            {
                return Ok("Admin deleted successfully");
            }
            return NotFound("Admin not found");
        }
    }
}
