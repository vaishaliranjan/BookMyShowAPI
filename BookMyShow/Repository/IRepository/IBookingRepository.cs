using BookMyShow.Models;
using System.Collections.Generic;

namespace BookMyShow.Repository.IRepository
{
    public interface IBookingRepository
    {
        List<Booking> GetAllBookings();
        void AddBooking(Booking booking);
    }
}
