using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyShow.Business
{
    public class VenueBusiness:IVenueBusiness
    {
        private readonly IVenueRepository venueRepository;
        public VenueBusiness(IVenueRepository venueRepository)
        {
            this.venueRepository = venueRepository;
        }
        public async Task CreateVenue(Venue venue)
        {
            await venueRepository.AddVenue(venue);
        }

        public async Task<List<Venue>> GetAllVenues()
        {
            return await venueRepository.GetAllVenues();
        }

        public async Task<Venue> GetVenue(int? id)
        {
            var venues =await GetAllVenues();
            var venue = venues.FirstOrDefault(v => v.VenueId == id);
            return venue;
        }

        public async Task<bool> BookVenue(int id)
        {
            var venue =await GetVenue(id);
            if (venue == null || venue.IsBooked==true)
            {
                return false;
            }
            venue.IsBooked = true;
            await venueRepository.UpdateVenue(venue);
            return true;
        }

        public async Task<bool> UnBookVenue(int id)
        {
            var venue = await GetVenue(id);
            if (venue == null || venue.IsBooked== false)
            {
                return false;
            }
            venue.IsBooked = false;
            await venueRepository.UpdateVenue(venue);
            return true;
        }
    }
}
