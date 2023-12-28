using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

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
        public IActionResult Get(int? id)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = currentUserId;
                if (User.IsInRole("Admin"))
                {
                    if (id == null)
                    {
                        var bookings = _bookingBusiness.GetAllBookings();
                        if(bookings == null)
                        {
                            return NotFound("Bookings not found");
                        }
                        return Ok(bookings);
                    }
                    else
                    {
                        var booking = _bookingBusiness.GetBooking(id);
                        if (booking == null)
                        {
                            return NotFound("Booking not found");
                        }
                        return Ok(booking);
                    }
                }
                else 
                {
                    if (id == null)
                    {
                        var bookings = _bookingBusiness.GetAllBookings(userId);
                        if (bookings==null)
                        {
                            return NotFound("Bookings not found");
                        }
                        return Ok(bookings);
                    }
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
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public IActionResult Post([FromBody] Booking booking)
        {
            try {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                booking.UserId = currentUserId;
                var e = _eventBusiness.GetEvent(booking.EventId);
                if (e == null)
                {
                    return NotFound("Event not found");
                }
                if (!_bookingBusiness.CreateBooking(booking, e))
                {
                    return BadRequest("Enter valid tickets. Number of tickets avilable = " + e.NumberOfTickets);
                }
                booking.UserId = currentUserId;
                _eventBusiness.DecrementTicket(booking.EventId, booking.NumberOfTickets);
                return Ok("Tickets booked successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
