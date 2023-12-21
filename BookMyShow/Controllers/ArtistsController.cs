using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private IArtistBusiness _artistBusiness;
        public ArtistsController(IArtistBusiness artistBusiness)
        {
            _artistBusiness = artistBusiness;
        }


        [HttpGet]

        public IActionResult Get(int? id)
        {
            if(id == null)
            {
                return Ok(_artistBusiness.GetAllArtists());
            }
            var artist = _artistBusiness.GetArtist(id);
            if(artist == null)
            {
                return NotFound("Artist not found!");
            }
            return Ok(artist);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_artistBusiness.CreateArtist(artist))
            {
                return Ok("Artist added successfully");
            }
            return BadRequest("Invalid DateTime");
        }
    }
}
