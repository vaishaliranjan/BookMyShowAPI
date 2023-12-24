using BookMyShow.Business;
using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Web;

namespace BookMyShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private IEventBusiness _eventBusiness;
        private IArtistBusiness _artistBusiness;
        private IVenueBusiness _venueBusiness;
        private IOrganizerBusiness _organizerBusiness;

        public EventsController(IEventBusiness eventBusiness, IArtistBusiness artistBusiness, IVenueBusiness venueBusiness, IOrganizerBusiness organizerBusiness)
        {
            _eventBusiness = eventBusiness;
            _artistBusiness = artistBusiness;
            _venueBusiness = venueBusiness;
            _organizerBusiness = organizerBusiness;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer,Organizer")]
        public IActionResult Get(int? id)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = currentUserId;
                // Check if the user is an admin
                if (User.IsInRole("Admin") || User.IsInRole("Customer"))
                {
                    // Admin can view all bookings
                    if (id == null)
                    {
                        return Ok(_eventBusiness.GetAllEvents());
                    }
                    // Admin can request a specific booking
                    else
                    {
                        var eventChoosen = _eventBusiness.GetEvent(id);
                        if (eventChoosen == null)
                        {
                            return NotFound("Event not found");
                        }
                        return Ok(eventChoosen);
                    }
                }
                // Check if the user is a customer
                else if (User.IsInRole("Organizer"))
                {
                    // Customer can only view their own bookings
                    if (id == null)
                    {
                        return Ok(_eventBusiness.GetAllEvents(userId));
                    }
                    // Customer can request a specific booking
                    else
                    {
                        var eventChoosen = _eventBusiness.GetEvent(id, userId);
                        if (eventChoosen == null)
                        {
                            return NotFound("Event not found");
                        }
                        return Ok(eventChoosen);
                    }
                }

                // If none of the conditions are met, return unauthorized or forbid
                return Forbid(); // Or return Unauthorized() depending on your desired behavior
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public IActionResult Post([FromBody] Event e)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = currentUserId;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var artist = _artistBusiness.GetArtist(e.ArtistId);
                var venue = _venueBusiness.GetVenue(e.VenueId);
                e.UserId = userId;
                if (artist != null && venue != null)
                {
                    _eventBusiness.CreateEvent(e);
                    var artistId = e.ArtistId;
                    var venueId = e.VenueId;
                    _artistBusiness.BookArtist(artistId);
                    _venueBusiness.BookVenue(venueId);
                    return Ok("Event added successfully");
                }
                return BadRequest("Invalid Request");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult Delete(int id)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = currentUserId;
                var e = _eventBusiness.GetEvent(id);
                bool result;
                if (User.IsInRole("Admin"))
                {
                    result = _eventBusiness.DeleteEvent(id);
                }
                else
                {
                    result = _eventBusiness.DeleteEvent(id, userId);
                }
                if (result)
                {
                    _artistBusiness.UnBookArtist(e.ArtistId);
                    _venueBusiness.UnBookVenue(e.VenueId);
                    return Ok("Event deleted successfully");
                }
                return NotFound("Event not found or tickets already booked!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
