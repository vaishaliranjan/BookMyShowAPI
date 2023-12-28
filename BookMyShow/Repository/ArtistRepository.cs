using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly AppDbContext _dbContext;
        public ArtistRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Artist> GetAllArtists()
        {
            return _dbContext.Artists.ToList();
        }


        public void AddArtist(Artist artist)
        {
            _dbContext.Artists.Add(artist);
            _dbContext.SaveChanges();
        }


        public void UpdateArtist(Artist artist)
        {
            var artistChoosen = _dbContext.Artists.FirstOrDefault(a=>a.Id==artist.Id);
                artistChoosen.IsBooked = artist.IsBooked;
                _dbContext.SaveChanges();
        }

    }
}
