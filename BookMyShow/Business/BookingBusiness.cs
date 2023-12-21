using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Data;
using BookMyShow.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Business
{
    public class BookingBusiness : IBookingBusiness
    {
        private AppDbContext _appDbContext;
        public BookingBusiness(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpPost]
        public bool CreateBooking(Booking booking, Event e )
        {
            if (booking.NumberOfTickets<=0 || booking.NumberOfTickets>e.NumberOfTickets)
            {
                return false;
            }
            booking.TotalPrice = e.Price * booking.NumberOfTickets;
            _appDbContext.Bookings.Add(booking);    
            _appDbContext.SaveChanges();
            return true;
        }
        public List<Booking> GetAllBookings(string customerId = null)
        {
            var bookings = _appDbContext.Bookings.ToList();
            if (customerId == null)
            {
                return bookings;
            }
            var customerBookings = bookings.FindAll(b => b.UserId == customerId).ToList();
            return customerBookings;
        }


        public Booking GetBooking(int? id, string customerId = null)
        {
            var booking = _appDbContext.Bookings.Find(id);
            if (booking == null)
            {
                return null;
            }
            if (customerId == null)
            {
                return booking;
            }
            var user = _appDbContext.Users.Find(customerId);
            if (user == null || user.IdentityUserId != customerId)
            {
                return null;
            }
            return booking;
        }
    }
}
