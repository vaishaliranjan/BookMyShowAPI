using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookMyShow
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private IBookingBusiness _bookingBusiness;
        private IEventBusiness _eventBusiness;
        private readonly UserManager<IdentityUser> _userManager;
        public BookingsController(IBookingBusiness bookingBusiness, IEventBusiness eventBusiness,UserManager<IdentityUser> userManager)
        {
            _bookingBusiness = bookingBusiness;
            _eventBusiness = eventBusiness;
            _userManager = userManager;
        }

/* To get bookings of all particular event this can be done by admin and customer*/
        [HttpGet("~/api/events/{eventId}/bookings")]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Get(int eventId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Admin"))
            {
                var bookings= await _bookingBusiness.GetAllBookingsByEventId(eventId);
                return StatusCode(StatusCodes.Status200OK,bookings);
            }
            else
            {
                var bookings = await _bookingBusiness.GetAllBookingsByEventId(eventId, currentUserId);
                return StatusCode(StatusCodes.Status200OK, bookings);
            }
        }

       
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Get(int? id)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (User.IsInRole("Admin"))
                {
                    if (id == null)
                    {
                        var bookings = await _bookingBusiness.GetAllBookings();
                        if(bookings == null)
                        {
                            return StatusCode(StatusCodes.Status404NotFound, "Bookings not found");
                        }
                        return StatusCode(StatusCodes.Status200OK, bookings);
                    }
                    else
                    {
                        var booking =await _bookingBusiness.GetBooking(id);
                        
                        if (booking == null)
                        {
                            return StatusCode(StatusCodes.Status404NotFound, "Booking not found");
                        }
                        return StatusCode(StatusCodes.Status200OK, booking);
                    }
                }
                else 
                {
                    if (id == null)
                    {
                        var bookings =await _bookingBusiness.GetAllBookings(currentUserId);
                        if (bookings==null)
                        {
                            return StatusCode(StatusCodes.Status404NotFound, "Bookings not found");
                        }
                        return StatusCode(StatusCodes.Status200OK, bookings);
                    }
                    else
                    {
                        var booking = await _bookingBusiness.GetBooking(id, currentUserId);
                        if (booking == null)
                        {
                            return StatusCode(StatusCodes.Status404NotFound, "Booking not found");
                        }
                        return StatusCode(StatusCodes.Status200OK, booking);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Post([FromBody] Booking booking)
        {
            try {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                booking.UserId = currentUserId;
                var e = await _eventBusiness.GetEvent(booking.EventId);
                if (e == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Event not found");
                }
                if (!await _bookingBusiness.CreateBooking(booking, e))
                {
                    return StatusCode(StatusCodes.Status400BadRequest,"Enter valid tickets");
                }
                booking.UserId = currentUserId;
                await _eventBusiness.DecrementTicket(booking.EventId, booking.NumberOfTickets);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
