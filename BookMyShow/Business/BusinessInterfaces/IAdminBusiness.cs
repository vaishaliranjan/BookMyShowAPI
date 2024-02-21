using System.Collections.Generic;
using System.Threading.Tasks;
using BookMyShow.Models;


namespace BookMyShow.Business.BusinessInterfaces
{
    public interface IAdminBusiness
    {
        public Task<List<User>> GetAllAdmins();
        public Task<User> GetAdmin(string id);
        public Task<bool> DeleteAdmin(string id);
    }
}
