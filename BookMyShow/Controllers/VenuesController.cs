using BookMyShow.Business;
using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                return Ok(_venueBusiness.GetAllVenues());
            }
            var venue = _venueBusiness.GetVenue(id);
            if (venue == null)
            {
                return NotFound("Venue not found!");
            }
            return Ok(venue);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Venue venue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _venueBusiness.CreateVenue(venue);
            return Ok("Venue added successfully");
        }
    }
}
