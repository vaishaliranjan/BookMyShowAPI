using BookMyShow.Business;
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
                // Check if the user is an admin
                if (User.IsInRole("Admin"))
                {
                    // Admin can view all bookings
                    if (id == null)
                    {
                        return Ok(_bookingBusiness.GetAllBookings());
                    }
                    // Admin can request a specific booking
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
                // Check if the user is a customer
                else if (User.IsInRole("Customer"))
                {
                    // Customer can only view their own bookings
                    if (id == null)
                    {
                        return Ok(_bookingBusiness.GetAllBookings(userId));
                    }
                    // Customer can request a specific booking
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

                // If none of the conditions are met, return unauthorized or forbid
                return Forbid(); // Or return Unauthorized() depending on your desired behavior
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
