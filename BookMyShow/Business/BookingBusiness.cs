using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<bool> CreateBooking(Booking booking, Event e )
        {
            if (booking.NumberOfTickets<=0 || booking.NumberOfTickets>e.NumberOfTickets)
            {
                return false;
            }
            booking.TotalPrice = e.Price * booking.NumberOfTickets;
            await bookingRepository.AddBooking(booking);
            return true;
        }
        public async Task<List<Booking>> GetAllBookings(string customerId = null)
        {
            var bookings = await bookingRepository.GetAllBookings();
            if (customerId == null)
            {
                return bookings;
            }
            var customerBookings = bookings.Where(b => b.UserId == customerId).ToList();
            return customerBookings;
        }


        public async Task<Booking> GetBooking(int? id, string customerId = null)
        {
            var bookings =await GetAllBookings();
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return null;
            }
            if (customerId == null)
            {
                return booking;
            }

            var users = await userRepository.GetAllUsers();
            var user = users.FirstOrDefault(u=>u.IdentityUserId == customerId);
            if (user == null || user.IdentityUserId != booking.UserId)
            {
                return null;
            }
            return booking;
        }
    }
}
