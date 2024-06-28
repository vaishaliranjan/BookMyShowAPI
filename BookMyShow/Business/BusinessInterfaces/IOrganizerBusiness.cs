using BookMyShow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyShow.Business.BusinessInterfaces
{
    public interface IOrganizerBusiness
    {
        public Task<List<User>> GetAllOrganizers();
        public Task<User> GetOrganizer(string id);
        public Task<bool> DeleteOrganizer(string id);

    }
}
