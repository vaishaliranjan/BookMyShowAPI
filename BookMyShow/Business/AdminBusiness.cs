using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using BookMyShow.Models.Enum;
using BookMyShow.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyShow.Business
{
    public class AdminBusiness : IAdminBusiness
    {
        private readonly IUserRepository userRepository;
        public AdminBusiness(IUserRepository userRepository)
        {
            this.userRepository = userRepository;           
        }

        public async Task<bool> DeleteAdmin(string id)
        {
            var admin =await GetAdmin(id);
            if (admin == null)
            {
                return false;
            }
            await userRepository.RemoveUser(admin);
            return true;
        }

        public async Task<User> GetAdmin(string id)
        {
            var admins = await GetAllAdmins();
            var admin =admins.FirstOrDefault(a => a.IdentityUserId.Equals(id));
            if (admin == null)
            {
                return null;
            }
            return admin;
        }

        public async Task<List<User>> GetAllAdmins()
        {
            var users = await userRepository.GetAllUsers();
            var admins = users.Where(u => u.Role == Role.Admin).ToList();
            return admins;
        }
    }
}
