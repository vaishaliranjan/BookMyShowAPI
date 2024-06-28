using BookMyShow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyShow.Business.BusinessInterfaces
{
    public interface IBookingBusiness
    {
        Task<BookingWithEvent> GetBooking(int? id, string customerId = null);
        Task<List<BookingWithEvent>> GetAllBookings(string customerId = null);
        Task<bool> CreateBooking(Booking booking, Event e);
        Task<List<BookingWithEvent>> GetAllBookingsByEventId(int eventId, string organizerId=null);
    }
}
