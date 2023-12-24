using BookMyShow.Models;
using System.Collections;
using System.Collections.Generic;

namespace BookMyShow.Repository.IRepository
{
    public interface IArtistRepository
    {
         List<Artist> GetAllArtists();                                                                       
         void AddArtist(Artist artist);
         void UpdateArtist(Artist artist);
    }
}
