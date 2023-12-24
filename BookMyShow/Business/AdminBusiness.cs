using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Models.Enum;
using BookMyShow.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Business
{
    public class AdminBusiness : IAdminBusiness
    {
        private readonly IUserRepository userRepository;
        public AdminBusiness(IUserRepository userRepository)
        {
            this.userRepository = userRepository;           
        }

        public bool DeleteAdmin(string id)
        {
            var admin = GetAdmin(id);
            if (admin == null)
            {
                return false;
            }
            userRepository.RemoveUser(admin);
            return true;
        }

        public User GetAdmin(string id)
        {
            var admins = GetAllAdmins();
            var admin = admins.FirstOrDefault(a => a.IdentityUserId.Equals(id));
            if (admin == null)
            {
                return null;
            }
            return admin;
        }

        public List<User> GetAllAdmins()
        {
            var users = userRepository.GetAllUsers();
            var admins = users.Where(u => u.Role == Role.Admin).ToList();
            return admins;
        }
    }
}
