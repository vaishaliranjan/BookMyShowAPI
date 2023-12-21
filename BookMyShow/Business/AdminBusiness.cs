using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Models.Enum;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Business
{
    public class AdminBusiness : IAdminBusiness
    {
        private AppDbContext _appDbContext;
        public AdminBusiness(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool DeleteAdmin(string id)
        {
            var admin = _appDbContext.Users.Find(id);
            if (admin == null)
            {
                return false;
            }
            _appDbContext.Users.Remove(admin);
            _appDbContext.SaveChanges();
            return true;
        }

        public User GetAdmin(string id)
        {
            var admin = _appDbContext.Users.Find(id);
            if(admin.Role == Role.Admin)
            {
                return admin;
            }
            return null;
        }

        public List<User> GetAllAdmins()
        {
            var users = _appDbContext.Users;
            var admins = users.Where(u => u.Role == Role.Admin).ToList();
            return admins;
        }
    }
}
