using System.Collections.Generic;
using BookMyShow.Models;


namespace BookMyShow.Business.BusinessInterfaces
{
    public interface IAdminBusiness
    {
        public List<User> GetAllAdmins();
        public User GetAdmin(string id);
        public bool DeleteAdmin(string id);
    }
}
