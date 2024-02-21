using BookMyShow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyShow.Repository.IRepository
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllBookings();
        Task AddBooking(Booking booking);
    }
}
