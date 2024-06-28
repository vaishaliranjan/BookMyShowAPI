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
        private readonly IEventRepository eventRepository;
        public BookingBusiness(IBookingRepository bookingRepository, IUserRepository userRepository, IEventRepository eventRepository)
        {
            this.bookingRepository = bookingRepository;
            this.userRepository = userRepository;
            this.eventRepository = eventRepository;
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
        /* public async Task<List<Booking>> GetAllBookings(string customerId = null)
         {
             var bookings = await bookingRepository.GetAllBookings();
             var events = await eventRepository.GetAllEvents();
             if (customerId == null)
             {
                 return bookings;
             }
             var customerBookings = bookings.Where(b => b.UserId == customerId).ToList();
             return customerBookings;
         }*/

        

        public async Task<List<BookingWithEvent>> GetAllBookings(string customerId = null)
        {
            var bookings = await bookingRepository.GetAllBookings();
            var events = await eventRepository.GetAllEvents();
            var users = await userRepository.GetAllUsers(); 
            var bookingsWithEventsAndUsers = bookings
                .Join(users,
                    booking => booking.UserId,
                    user => user.IdentityUserId,
                    (booking, user) => new { Booking = booking, User = user })
                .Select(joinResult => new BookingWithEvent
                {
                    Booking = joinResult.Booking,
                    Event = events.FirstOrDefault(e => e.Id == joinResult.Booking.EventId),
                    Username = joinResult.User.Username,
                    UserId= joinResult.User.IdentityUserId
                })
                .ToList();

            if (customerId == null)
            {
                return bookingsWithEventsAndUsers;
            }

            var customerBookingsWithEventsAndUsers = bookingsWithEventsAndUsers
                .Where(bwe => bwe.Booking.UserId == customerId)
                .ToList();

            return customerBookingsWithEventsAndUsers;
        }

        public async Task<List<BookingWithEvent>> GetAllBookingsByEventId(int eventId, string organizerId = null)
        {
            var bookings = await GetAllBookings();
            var bookingsWithEventId= bookings.Where(b=>b.Event.Id ==eventId).ToList();  
            if (organizerId == null)
            {
                return bookingsWithEventId;
            }
            var bookingsWithEventIdOfOrganizers=  bookingsWithEventId.Where(b=>b.Event.UserId == organizerId).ToList();   
            return bookingsWithEventIdOfOrganizers;
        }

        public async Task<BookingWithEvent> GetBooking(int? id, string customerId = null)
        {
            var bookings =await GetAllBookings();
            var booking = bookings.FirstOrDefault(b => b.Booking.Id == id);
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
            if (user == null || user.IdentityUserId != booking.Booking.UserId)
            {
                return null;
            }
            return booking;
        }
    }
}
