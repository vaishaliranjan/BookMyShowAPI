using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Data;
using BookMyShow.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BookMyShow.Business
{
    public class ArtistBusiness: IArtistBusiness
    {
        private AppDbContext _appcontext;
        public ArtistBusiness(AppDbContext appDbContext)
        {
            _appcontext = appDbContext;
        }
        
        public bool CreateArtist(Artist artist)
        {
            DateTime timing;
            var isValidDate = DateTime.TryParseExact(artist.Timing,"dd-MM-yyyyTHH:mm:ss",CultureInfo.InvariantCulture, DateTimeStyles.None, out timing);
            var today= DateTime.Now;
            if(!isValidDate || timing <today )
            {
                return false;
            }
            _appcontext.Artists.Add(artist);
            _appcontext.SaveChanges();
            return true;
        }

        public List<Artist> GetAllArtists()
        {
            return _appcontext.Artists.ToList();
        }

        public Artist GetArtist(int? id)
        {
            var artist = _appcontext.Artists.Find(id);
            return artist;
        }
        

        public bool BookArtist(int id)
        {
            var artist = _appcontext.Artists.FirstOrDefault(a=>a.Id==id);
            if(artist == null || artist.IsBooked==true)
            {
                return false;
            }
            artist.IsBooked = true;
            _appcontext.SaveChanges();
            return true;
        }

        public bool UnBookArtist(int id)
        {
            var artist = _appcontext.Artists.FirstOrDefault(a => a.Id == id);
            if (artist == null)
            {
                return false;
            }
            artist.IsBooked = false;
            _appcontext.SaveChanges();
            return true;
        }
    }
}
