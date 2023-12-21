using BookMyShow.Models;
using System.Collections.Generic;

namespace BookMyShow.Business.BusinessInterfaces
{
    public interface IVenueBusiness
    {
        public void CreateVenue(Venue venue);
        public bool BookVenue(int id);
        public List<Venue> GetAllVenues();
        public Venue GetVenue(int? id);
        public bool UnBookVenue(int id);
    }
}
