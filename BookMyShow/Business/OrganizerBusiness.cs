using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Models.Enum;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Business
{
    public class OrganizerBusiness: IOrganizerBusiness
    {
        private AppDbContext _appDbContext;
        public OrganizerBusiness(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        } 
        public User GetOrganizer(string id)
        {
            var organizer = _appDbContext.Users.Find(id);
            if (organizer.Role == Role.Organizer)
            {
                return organizer;
            }
            return null;
        }

        public List<User> GetAllOrganizers()
        {
            var users = _appDbContext.Users;
            var organizers = users.Where(u => u.Role == Role.Organizer).ToList();
            return organizers;
        }
    }
}

