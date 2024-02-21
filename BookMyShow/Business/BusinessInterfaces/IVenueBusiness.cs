using BookMyShow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyShow.Business.BusinessInterfaces
{
    public interface IVenueBusiness
    {
        public Task CreateVenue(Venue venue);
        public Task<bool> BookVenue(int id);
        public Task<List<Venue>> GetAllVenues();
        public Task<Venue> GetVenue(int? id);
        public Task<bool> UnBookVenue(int id);
    }
}
