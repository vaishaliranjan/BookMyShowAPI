using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using BookMyShow.Models.Enum;
using BookMyShow.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyShow.Business
{
    public class OrganizerBusiness: IOrganizerBusiness
    {
        private readonly IUserRepository userRepository;
        public OrganizerBusiness(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<User> GetOrganizer(string id)
        {
            var organizers = await GetAllOrganizers();
            var organizer = organizers.FirstOrDefault(o => o.IdentityUserId.Equals(id));
            if (organizer == null)
            {
                return null;
            }
            return organizer;
        }

        public async Task<List<User>> GetAllOrganizers()
        {
            var users =await userRepository.GetAllUsers();
            var organizers = users.Where(u => u.Role == Role.Organizer).ToList();
            return organizers;
        }

        public async Task<bool> DeleteOrganizer(string id)
        {
            var admin = await GetOrganizer(id);
            if (admin == null)
            {
                return false;
            }
            await userRepository.RemoveUser(admin);
            return true;
        }
    }
}

