using BookMyShow.Business;
using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get(string id)
        {
            if (id == null)
            {
                return Ok(_organizerBusiness.GetAllOrganizers());
            }
            var organizer = _organizerBusiness.GetOrganizer(id);
            if (organizer == null)
            {
                return NotFound("Organizer not found!");
            }
            return Ok(organizer);
        }

    }
}
