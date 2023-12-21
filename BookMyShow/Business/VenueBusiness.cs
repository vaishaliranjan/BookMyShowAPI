using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Data;
using BookMyShow.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Business
{
    public class VenueBusiness:IVenueBusiness
    {
        private AppDbContext _appcontext;
        public VenueBusiness(AppDbContext appDbContext)
        {
            _appcontext = appDbContext;
        }
        public void CreateVenue(Venue venue)
        {
            _appcontext.Venues.Add(venue);
            _appcontext.SaveChanges();
        }

        public List<Venue> GetAllVenues()
        {
            return _appcontext.Venues.ToList(); ;
        }

        public Venue GetVenue(int? id)
        {
            var venue = _appcontext.Venues.Find(id);
            return venue;
        }


        public bool BookVenue(int id)
        {
            var venue = _appcontext.Venues.Find(id);
            if (venue == null || venue.IsBooked==true)
            {
                return false;
            }
            venue.IsBooked = true;
            _appcontext.SaveChanges();
            return true;
        }

        public bool UnBookVenue(int id)
        {
            var venue = _appcontext.Venues.FirstOrDefault(v=>v.VenueId== id);
            if (venue == null)
            {
                return false;
            }
            venue.IsBooked = false;
            _appcontext.SaveChanges();
            return true;
        }
    }
}
