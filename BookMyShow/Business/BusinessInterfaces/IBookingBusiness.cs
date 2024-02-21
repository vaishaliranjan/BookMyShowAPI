using BookMyShow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyShow.Business.BusinessInterfaces
{
    public interface IBookingBusiness
    {
        Task<Booking> GetBooking(int? id, string customerId = null);
        Task<List<Booking>> GetAllBookings(string customerId = null);
        Task<bool> CreateBooking(Booking booking, Event e);
    }
}
