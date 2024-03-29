﻿using BookMyShow.Business.BusinessInterfaces;
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
                    var artists = _artistBusiness.GetAllArtists();
                    if(artists == null)
                    {
                        return NotFound("Artists not found!");
                    }
                    return Ok(artists);
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
        public async Task<IActionResult> Post([FromBody] Artist artist)
        {
            try
            {
                //custom validation
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _artistBusiness.CreateArtist(artist))
                {
                    return Ok("Artist added successfully");//201
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
