using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyShow.Business
{
    public class ArtistBusiness: IArtistBusiness
    {
        private readonly IArtistRepository artistRepository;
        public ArtistBusiness(IArtistRepository artistRepository)
        {
            this.artistRepository = artistRepository;
        }

        public async Task<bool> CreateArtist(Artist artist)
        {//custom exception -> generic exception-> specific 

         
            DateTime timing;
            var isValidDate = DateTime.TryParseExact(artist.Timing,"dd-MM-yyyyTHH:mm:ss",CultureInfo.InvariantCulture, DateTimeStyles.None, out timing);
            var today= DateTime.Now;
            if(!isValidDate || timing <today )
            {
                return false;
            }
            await artistRepository.AddArtist(artist);
            return true;
        }

        public async Task<List<Artist>> GetAllArtists()
        {
            var artists= await artistRepository.GetAllArtists();
            foreach (var artist in artists)
            {
                DateTime timing;
                var isValidDate = DateTime.TryParseExact(artist.Timing, "dd-MM-yyyyTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out timing);
                var today = DateTime.Now;
                if (!isValidDate || timing < today)
                {
                    artist.IsBooked = true;
                }
            }
            return artists;
        }

        public async Task<Artist> GetArtist(int? id)
        {
            var artists = await GetAllArtists();
            var artist = artists.FirstOrDefault(a => a.Id == id);
            return artist;
        }
        

        public async Task<bool> BookArtist(int id)
        {
            var artist = await GetArtist(id);
            if(artist == null || artist.IsBooked==true)
            {
                return false;
            }
            artist.IsBooked = true;
            await artistRepository.UpdateArtist(artist);
            return true;
        }

        public async Task<bool> UnBookArtist(int id)
        {
            var artist = await GetArtist(id);
            if (artist == null || artist.IsBooked==false)
            {
                return false;
            }
            artist.IsBooked = false;
            await artistRepository.UpdateArtist(artist);
            return true;
        }
    }
}
