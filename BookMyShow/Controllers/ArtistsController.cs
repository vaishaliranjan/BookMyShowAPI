using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookMyShow
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
        public async Task<IActionResult> Get(int? id)
        {
            try
            {
                if (id == null)
                {
                    var artists =await  _artistBusiness.GetAllArtists();
                    if(artists == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Artists not found");
                    }
                    return StatusCode(StatusCodes.Status200OK, artists);
                }
                var artist =await _artistBusiness.GetArtist(id);
                if (artist == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Artist not found");
                }
                return StatusCode(StatusCodes.Status404NotFound, artist);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] Artist artist)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _artistBusiness.CreateArtist(artist))
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid DateTime"); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
