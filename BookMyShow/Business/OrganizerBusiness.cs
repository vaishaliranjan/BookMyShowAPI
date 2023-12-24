using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Models.Enum;
using BookMyShow.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Business
{
    public class OrganizerBusiness: IOrganizerBusiness
    {
        private readonly IUserRepository userRepository;
        public OrganizerBusiness(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public User GetOrganizer(string id)
        {
            var organizers = GetAllOrganizers();
            var organizer = organizers.FirstOrDefault(o => o.IdentityUserId.Equals(id));
            if (organizer == null)
            {
                return null;
            }
            return organizer;
        }

        public List<User> GetAllOrganizers()
        {
            var users = userRepository.GetAllUsers();
            var organizers = users.Where(u => u.Role == Role.Organizer).ToList();
            return organizers;
        }
    }
}

