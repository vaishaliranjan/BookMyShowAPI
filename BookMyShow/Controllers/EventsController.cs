using BookMyShow.Business;
using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookMyShow
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
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (User.IsInRole("Admin") || User.IsInRole("Customer"))
                {
                    if (id == null)
                    {
                        var bookings = await _eventBusiness.GetAllEvents();
                        if(bookings == null)
                        {
                            return StatusCode(StatusCodes.Status404NotFound, "Bookings not found");
                        }
                        return StatusCode(StatusCodes.Status200OK, bookings);
                    }
                    else
                    {
                        var eventChoosen = await _eventBusiness.GetEvent(id);
                        if (eventChoosen == null)
                        {
                            return StatusCode(StatusCodes.Status404NotFound, "Event not found");
                        }
                        return StatusCode(StatusCodes.Status200OK, eventChoosen); ;
                    }
                }
                else
                {
                    if (id == null)
                    {
                        var bookings =await _eventBusiness.GetAllEvents(currentUserId);
                        if (bookings == null)
                        {
                            return StatusCode(StatusCodes.Status404NotFound, "Booking not found");
                        }
                        return StatusCode(StatusCodes.Status200OK, bookings);
                    }

                    else
                    {
                        var eventChoosen = await _eventBusiness.GetEvent(id, currentUserId);
                        if (eventChoosen == null)
                        {
                            return StatusCode(StatusCodes.Status404NotFound, "Event not found");
                        }
                        return StatusCode(StatusCodes.Status200OK, eventChoosen);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /* To get events of all particular artist this can be done by admin and organizer*/
        [HttpGet("~/api/artists/{artistUsername}/events")]
        [Authorize(Roles = "Admin,Organizer,Customer")]

        public async Task<IActionResult> Get(string artistUsername)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Admin") || User.IsInRole("Customer"))
            {
                var events = await _eventBusiness.GetAllEventsByArtistUsername(artistUsername);
                return StatusCode(StatusCodes.Status200OK, events);
            }
            else
            {
                var events = await _eventBusiness.GetAllEventsByArtistUsername(artistUsername,currentUserId);
                return StatusCode(StatusCodes.Status200OK, events);
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
                DateTime artistTime;
                var isValidDate = DateTime.TryParseExact(artist.Timing, "dd-MM-yyyyTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out artistTime);
                var today = DateTime.Now;
                if (!isValidDate || artistTime < today)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,"This artist can't be choosen");
                }
                e.UserId = currentUserId;
                if (artist != null && venue != null)
                {
                    e.InitialTickets = e.NumberOfTickets;
                    e.ArtistUsername = artist.ArtistUsername;
                    await _eventBusiness.CreateEvent(e);
                    var artistId = e.ArtistId;
                    var venueId = e.VenueId;
                    await _artistBusiness.BookArtist(artistId);
                    await _venueBusiness.BookVenue(venueId);
                    return StatusCode(StatusCodes.Status201Created);
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
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                return StatusCode(StatusCodes.Status406NotAcceptable, "Tickets already booked"); ;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
