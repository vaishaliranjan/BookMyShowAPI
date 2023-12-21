using BookMyShow.Models;
using System.Collections.Generic;

namespace BookMyShow.Business.BusinessInterfaces
{
    public interface IBookingBusiness
    {
        Booking GetBooking(int? id, string customerId = null);
        List<Booking> GetAllBookings(string customerId = null);
        bool CreateBooking(Booking booking, Event e);
    }
}
