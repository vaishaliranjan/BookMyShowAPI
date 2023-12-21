using BookMyShow.Business;
using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public EventsController(IEventBusiness eventBusiness, IArtistBusiness artistBusiness, IVenueBusiness venueBusiness)
        {
            _eventBusiness = eventBusiness;
            _artistBusiness = artistBusiness;
            _venueBusiness = venueBusiness;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult Get(int? id, string userId)
        {
            //admin viewing events
            if(id==null && userId == null)
            {
                return Ok(_eventBusiness.GetAllEvents()); 
            }
            //organizer viewing events
            else if(id==null && userId!=null)
            {
                return Ok(_eventBusiness.GetAllEvents(userId));
            }
            //admin-requesting specific event
            else if(id!=null && userId==null)
            {
                var eventChoosen= _eventBusiness.GetEvent(id);
                if (eventChoosen == null)
                {
                    return NotFound("Event not found");
                }
                return Ok(eventChoosen);
            }
            //organizer requesting speific event 
            else
            {
                var eventChoosen = _eventBusiness.GetEvent(id,userId);
                if (eventChoosen == null)
                {
                    return NotFound("Event not found");
                }
                return Ok(eventChoosen);
            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult Post([FromBody] Event e)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var artist = _artistBusiness.GetArtist(e.ArtistId);
            var venue = _venueBusiness.GetVenue(e.VenueId);
            if(artist !=null && venue != null)
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

        [HttpDelete]
        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult Delete(int id, string organizerId)
        {
            var e= _eventBusiness.GetEvent(id);
            bool result;
            if (organizerId == null)
            {
                result = _eventBusiness.DeleteEvent(id);
            }
            else
            {
                result = _eventBusiness.DeleteEvent(id, organizerId);
            }
            if (result)
            {
                _artistBusiness.UnBookArtist(e.ArtistId);
                _venueBusiness.UnBookVenue(e.VenueId);
                return Ok("Event deleted successfully");
            }
            return NotFound("Event not found or tickets already booked!");
        }
    }
}
