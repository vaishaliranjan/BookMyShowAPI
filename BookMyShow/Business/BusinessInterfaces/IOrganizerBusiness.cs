using BookMyShow.Models;
using System.Collections.Generic;

namespace BookMyShow.Business.BusinessInterfaces
{
    public interface IOrganizerBusiness
    {
        public List<User> GetAllOrganizers();
        public User GetOrganizer(string id);
    }
}
