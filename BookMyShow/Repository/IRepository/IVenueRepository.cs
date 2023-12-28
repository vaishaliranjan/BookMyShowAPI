using BookMyShow.Models;
using System.Collections.Generic;

namespace BookMyShow.Repository.IRepository
{
    public interface IVenueRepository
    {
        public List<Venue> GetAllVenues();
        public void AddVenue(Venue venue);
        public void UpdateVenue(Venue venue);
    }
}
