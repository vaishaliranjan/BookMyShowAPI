using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyShow.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _dbContext;
        public BookingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            return await _dbContext.Bookings.ToListAsync();
        }

        public async Task AddBooking(Booking booking)
        {
            await _dbContext.Bookings.AddAsync(booking);
            await _dbContext.SaveChangesAsync();
        }
    }
}
