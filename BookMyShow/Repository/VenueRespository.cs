using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyShow.Repository
{
    public class VenueRespository: IVenueRepository
    {
        private readonly AppDbContext _dbContext;
        public VenueRespository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Venue>> GetAllVenues()
        {
            return await _dbContext.Venues.ToListAsync();
        }

        public async Task AddVenue(Venue venue)
        {
            await _dbContext.Venues.AddAsync(venue);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateVenue(Venue venue)
        {
            var venueChoosen = await _dbContext.Venues.FirstOrDefaultAsync(v=>v.VenueId==venue.VenueId);
            venueChoosen.IsBooked = venue.IsBooked;
            await _dbContext.SaveChangesAsync();
        }

      
    }
}
