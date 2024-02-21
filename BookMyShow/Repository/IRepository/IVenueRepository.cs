using BookMyShow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyShow.Repository.IRepository
{
    public interface IVenueRepository
    {
        Task<List<Venue>> GetAllVenues();
        Task AddVenue(Venue venue);
        Task UpdateVenue(Venue venue);
    }
}
