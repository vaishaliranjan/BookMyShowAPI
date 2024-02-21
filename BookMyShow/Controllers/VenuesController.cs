using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookMyShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private IVenueBusiness _venueBusiness;
        public VenuesController(IVenueBusiness venueBusiness)
        {
            _venueBusiness = venueBusiness;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Get(int? id)
        {
            try
            {
                if (id == null)
                {
                    var venues = await _venueBusiness.GetAllVenues();
                    if(venues== null)
                    {
                        return NotFound("Venues not found!");
                    }
                    return Ok(venues);
                }
                var venue = await _venueBusiness.GetVenue(id);
                if (venue == null)
                {
                    return NotFound("Venue not found!");
                }
                return Ok(venue);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] Venue venue)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _venueBusiness.CreateVenue(venue);
                return Ok("Venue added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
