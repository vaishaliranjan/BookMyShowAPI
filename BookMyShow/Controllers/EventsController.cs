using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookMyShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventBusiness _eventBusiness;
        private readonly IArtistBusiness _artistBusiness;
        private readonly IVenueBusiness _venueBusiness;
        private readonly IOrganizerBusiness _organizerBusiness;

        public EventsController(IEventBusiness eventBusiness, IArtistBusiness artistBusiness, IVenueBusiness venueBusiness, IOrganizerBusiness organizerBusiness)
        {
            _eventBusiness = eventBusiness;
            _artistBusiness = artistBusiness;
            _venueBusiness = venueBusiness;
            _organizerBusiness = organizerBusiness;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer,Organizer")]
        public async Task<IActionResult> Get(int? id)
        {
            try
            {
                //var user = user.Identity.GetUserId();-> Doubt to ritika + bookings
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (User.IsInRole("Admin") || User.IsInRole("Customer"))
                {
                    if (id == null)
                    {
                        var bookings = await _eventBusiness.GetAllEvents();
                        if(bookings == null)
                        {
                            return NotFound("Bookings not found!");
                        }
                        return Ok(bookings);
                    }
                    else
                    {
                        var eventChoosen = await _eventBusiness.GetEvent(id);
                        if (eventChoosen == null)
                        {
                            return NotFound("Event not found");
                        }
                        return Ok(eventChoosen);
                    }
                }
                else
                {
                    if (id == null)
                    {
                        var bookings =await _eventBusiness.GetAllEvents(currentUserId);
                        if (bookings == null)
                        {
                            return NotFound("Bookings not found!");
                        }
                        return Ok(bookings);
                    }

                    else
                    {
                        var eventChoosen = await _eventBusiness.GetEvent(id, currentUserId);
                        if (eventChoosen == null)
                        {
                            return NotFound("Event not found");
                        }
                        return Ok(eventChoosen);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Post([FromBody] Event e)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var artist = await _artistBusiness.GetArtist(e.ArtistId);
                var venue = await _venueBusiness.GetVenue(e.VenueId);
                e.UserId = currentUserId;
                if (artist != null && venue != null)
                {
                    e.InitialTickets = e.NumberOfTickets;
                    await _eventBusiness.CreateEvent(e);
                    var artistId = e.ArtistId;
                    var venueId = e.VenueId;
                    await _artistBusiness.BookArtist(artistId);
                    await _venueBusiness.BookVenue(venueId);
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = currentUserId;
                var e =await _eventBusiness.GetEvent(id);
                bool result;
                if (User.IsInRole("Admin"))
                {
                    result = await _eventBusiness.DeleteEvent(id);
                }
                else
                {
                    result = await _eventBusiness.DeleteEvent(id, userId);
                }
                if (result)
                {
                    await _artistBusiness.UnBookArtist(e.ArtistId);
                    await _venueBusiness.UnBookVenue(e.VenueId);
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
