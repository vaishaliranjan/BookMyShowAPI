using BookMyShow.Business;
using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private IBookingBusiness _bookingBusiness;
        private IEventBusiness _eventBusiness;
        public BookingsController(IBookingBusiness bookingBusiness, IEventBusiness eventBusiness)
        {
            _bookingBusiness = bookingBusiness;
            _eventBusiness = eventBusiness;
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public IActionResult Get(int? id, string userId)
        {
            //admin viewing bookings
            if (id == null && userId == null)
            {
                return Ok(_bookingBusiness.GetAllBookings());
            }
            //customer viewing bookings
            else if (id == null && userId != null)
            {
                return Ok(_bookingBusiness.GetAllBookings(userId));
            }
            //admin-requesting specific booking
            else if (id != null && userId == null)
            {
                var booking =_bookingBusiness.GetBooking(id);
                if (booking == null)
                {
                    return NotFound("Booking not found");
                }
                return Ok(booking);
            }
            //customer requesting speific booking 
            else
            {
                var booking = _bookingBusiness.GetBooking(id, userId);
                if (booking == null)
                {
                    return NotFound("Booking not found");
                }
                return Ok(booking);
            }

        }


        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        public IActionResult Post([FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var e = _eventBusiness.GetEvent(booking.EventId);
            if(e == null)
            {
                return NotFound("Event not found");
            }
            if(!_bookingBusiness.CreateBooking(booking, e))
            {
                return BadRequest("Enter valid tickets. Number of tickets avilable = "+e.NumberOfTickets);
            }
            _eventBusiness.DecrementTicket(booking.EventId, booking.NumberOfTickets);
            return Ok("Tickets booked successfully");
        }


    }
}
