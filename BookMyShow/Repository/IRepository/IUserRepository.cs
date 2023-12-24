using BookMyShow.Models;
using System.Collections;
using System.Collections.Generic;

namespace BookMyShow.Repository.IRepository
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void AddUser(User user);
        void RemoveUser(User user);
    }
}
