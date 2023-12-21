using BookMyShow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyShow.Business.BusinessInterfaces
{
    public interface IArtistBusiness
    {
        public List<Artist> GetAllArtists();
        public Artist GetArtist(int? id);
        public bool CreateArtist(Artist artist);
        public bool BookArtist(int id);
        public bool UnBookArtist(int id);
    }
}
