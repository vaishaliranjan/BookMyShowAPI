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
                            return NotFound("Bookings not found");
                        }
                        return Ok(bookings);
                    }
                    else
                    {
                        var booking =await _bookingBusiness.GetBooking(id);
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
                        var bookings =await _bookingBusiness.GetAllBookings(currentUserId);
                        if (bookings==null)
                        {
                            return NotFound("Bookings not found");
                        }
                        return Ok(bookings);
                    }
                    else
                    {
                        var booking = await _bookingBusiness.GetBooking(id, currentUserId);
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
                    return NotFound("Event not found");
                }
                if (!await _bookingBusiness.CreateBooking(booking, e))
                {
                    return BadRequest("Enter valid tickets. Number of tickets avilable = " + e.NumberOfTickets);
                }
                booking.UserId = currentUserId;
                await _eventBusiness.DecrementTicket(booking.EventId, booking.NumberOfTickets);
                return Ok("Tickets booked successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
