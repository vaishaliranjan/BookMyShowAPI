using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Repository
{
    public class VenueRespository: IVenueRepository
    {
        private readonly AppDbContext _dbContext;
        public VenueRespository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Venue> GetAllVenues()
        {
            return _dbContext.Venues.ToList();
        }


        public void AddVenue(Venue venue)
        {
            _dbContext.Venues.Add(venue);
            _dbContext.SaveChanges();
        }

        public void UpdateVenue(Venue venue)
        {
            var venueChoosen = _dbContext.Venues.Find(venue.VenueId);
            venueChoosen.IsBooked=venue.IsBooked;
            _dbContext.SaveChanges();
        }
    }
}
