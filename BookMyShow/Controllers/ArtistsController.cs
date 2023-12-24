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
    public class ArtistsController : ControllerBase
    {
        private IArtistBusiness _artistBusiness;
        public ArtistsController(IArtistBusiness artistBusiness)
        {
            _artistBusiness = artistBusiness;
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult Get(int? id)
        {
            try
            {
                if (id == null)
                {
                    return Ok(_artistBusiness.GetAllArtists());
                }
                var artist = _artistBusiness.GetArtist(id);
                if (artist == null)
                {
                    return NotFound("Artist not found!");
                }
                return Ok(artist);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] Artist artist)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
