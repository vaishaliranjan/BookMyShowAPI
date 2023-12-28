using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Business
{
    public class VenueBusiness:IVenueBusiness
    {
        private readonly IVenueRepository venueRepository;
        public VenueBusiness(IVenueRepository venueRepository)
        {
            this.venueRepository = venueRepository;
        }
        public void CreateVenue(Venue venue)
        {
            venueRepository.AddVenue(venue);
        }

        public List<Venue> GetAllVenues()
        {
            return venueRepository.GetAllVenues();
        }

        public Venue GetVenue(int? id)
        {
            var venues = GetAllVenues();
            var venue = venues.FirstOrDefault(v => v.VenueId == id);
            return venue;
        }

        public bool BookVenue(int id)
        {
            var venue = GetVenue(id);
            if (venue == null || venue.IsBooked==true)
            {
                return false;
            }
            venue.IsBooked = true;
            venueRepository.UpdateVenue(venue);
            return true;
        }

        public bool UnBookVenue(int id)
        {
            var venue = GetVenue(id);
            if (venue == null || venue.IsBooked== false)
            {
                return false;
            }
            venue.IsBooked = false;
            venueRepository.UpdateVenue(venue);
            return true;
        }
    }
}
