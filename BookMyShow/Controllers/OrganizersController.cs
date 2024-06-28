using BookMyShow.Business;
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
                        return StatusCode(StatusCodes.Status404NotFound, "Organizers not found"); ;
                    }
                    return  StatusCode(StatusCodes.Status200OK, organizers);
                }
                var organizer =await _organizerBusiness.GetOrganizer(id);
                if (organizer == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Organizer not found"); ;
                }
                return StatusCode(StatusCodes.Status200OK,organizer); ;
            }
            catch (Exception ex)
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
                var result = await _organizerBusiness.DeleteOrganizer(id);
                if (result)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                return StatusCode(StatusCodes.Status404NotFound, "Organizer not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
