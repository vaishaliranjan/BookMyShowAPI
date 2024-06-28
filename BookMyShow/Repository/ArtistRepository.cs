using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyShow.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly AppDbContext _dbContext;
        public ArtistRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Artist>> GetAllArtists()
        {
            return await _dbContext.Artists.ToListAsync();
        }


        public async Task AddArtist(Artist artist)
        {
            await _dbContext.Artists.AddAsync(artist);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateArtist(Artist artist)
        {
            var artistChoosen = await _dbContext.Artists.FirstOrDefaultAsync(a=>a.Id==artist.Id);
            artistChoosen.IsBooked = artist.IsBooked;
            await _dbContext.SaveChangesAsync();
        }

    }
}
