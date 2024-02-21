using BookMyShow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyShow.Repository.IRepository
{
    public interface IArtistRepository
    {
         Task<List<Artist>> GetAllArtists();                                                                       
         Task AddArtist(Artist artist);
         Task UpdateArtist(Artist artist);
    }
}
