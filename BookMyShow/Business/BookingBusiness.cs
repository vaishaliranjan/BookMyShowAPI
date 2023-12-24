using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Business
{
    public class BookingBusiness : IBookingBusiness
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IUserRepository userRepository;
        public BookingBusiness(IBookingRepository bookingRepository, IUserRepository userRepository)
        {
            this.bookingRepository = bookingRepository;
            this.userRepository = userRepository;
        }

        public bool CreateBooking(Booking booking, Event e )
        {
            if (booking.NumberOfTickets<=0 || booking.NumberOfTickets>e.NumberOfTickets)
            {
                return false;
            }
            booking.TotalPrice = e.Price * booking.NumberOfTickets;
            bookingRepository.AddBooking(booking);
            return true;
        }
        public List<Booking> GetAllBookings(string customerId = null)
        {
            var bookings = bookingRepository.GetAllBookings();
            if (customerId == null)
            {
                return bookings;
            }
            var customerBookings = bookings.FindAll(b => b.UserId == customerId).ToList();
            return customerBookings;
        }


        public Booking GetBooking(int? id, string customerId = null)
        {
            var bookings = GetAllBookings();
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return null;
            }
            if (customerId == null)
            {
                return booking;
            }

            var users = userRepository.GetAllUsers();
            var user = users.FirstOrDefault(u=>u.IdentityUserId == customerId);
            if (user == null || user.IdentityUserId != customerId)
            {
                return null;
            }
            return booking;
        }
    }
}
