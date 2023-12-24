using BookMyShow.Business;
using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult Get(string id)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
