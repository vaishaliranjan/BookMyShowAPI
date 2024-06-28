using BookMyShow.Business.BusinessInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookMyShow
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
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                if (id == null)
                {
                    var admins =await _adminBusiness.GetAllAdmins();
                    if (admins == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Admins not found");
                    }
                    return StatusCode(StatusCodes.Status200OK, admins);
                }
                var admin =await  _adminBusiness.GetAdmin(id);
                if (admin == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Admin not found");
                }
                return StatusCode(StatusCodes.Status200OK, admin);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _adminBusiness.DeleteAdmin(id);
                if (result)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                return StatusCode(StatusCodes.Status404NotFound, "Admin not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
