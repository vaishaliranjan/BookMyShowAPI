using BookMyShow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyShow.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task AddUser(User user);
        Task RemoveUser(User user);
    }
}
