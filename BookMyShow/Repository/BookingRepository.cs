using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _dbContext;
        public BookingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Booking> GetAllBookings()
        {
            return _dbContext.Bookings.ToList();
        }

        public void AddBooking(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();
        }
    }
}
