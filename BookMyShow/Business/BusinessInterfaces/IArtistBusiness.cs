using BookMyShow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyShow.Business.BusinessInterfaces
{
    public interface IArtistBusiness
    {
        public Task<List<Artist>> GetAllArtists();
        public Task<Artist> GetArtist(int? id);
        public Task<bool> CreateArtist(Artist artist);
        public Task<bool> BookArtist(int id);
        public Task<bool> UnBookArtist(int id);
    }
}
