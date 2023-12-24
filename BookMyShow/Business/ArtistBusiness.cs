using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BookMyShow.Business
{
    public class ArtistBusiness: IArtistBusiness
    {
        private readonly IArtistRepository artistRepository;
        public ArtistBusiness(IArtistRepository artistRepository)
        {
            this.artistRepository = artistRepository;
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
            artistRepository.AddArtist(artist);
            return true;
        }

        public List<Artist> GetAllArtists()
        {
            return artistRepository.GetAllArtists();
        }

        public Artist GetArtist(int? id)
        {
            var artists = GetAllArtists();
            var artist = artists.FirstOrDefault(a => a.Id == id);
            return artist;
        }
        

        public bool BookArtist(int id)
        {
            var artist = GetArtist(id);
            if(artist == null || artist.IsBooked==true)
            {
                return false;
            }
            artist.IsBooked = true;
            artistRepository.UpdateArtist(artist);
            return true;
        }

        public bool UnBookArtist(int id)
        {
            var artist = GetArtist(id);
            if (artist == null)
            {
                return false;
            }
            artist.IsBooked = false;
            artistRepository.UpdateArtist(artist);
            return true;
        }
    }
}
