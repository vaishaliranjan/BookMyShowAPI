using BookMyShow.Business.BusinessInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookMyShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizersController : ControllerBase
    {
        private IOrganizerBusiness _organizerBusiness;
        public OrganizersController(IOrganizerBusiness organizerBusiness)
        {
            _organizerBusiness = organizerBusiness;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                if (id == null)
                {
                    var organizers = await _organizerBusiness.GetAllOrganizers();
                    if (organizers == null)
                    {
                        return NotFound("Organizers not found!");
                    }
                    return Ok(organizers);
                }
                var organizer =await _organizerBusiness.GetOrganizer(id);
                if (organizer == null)
                {
                    return NotFound("Organizer not found!");
                }
                return Ok(organizer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
