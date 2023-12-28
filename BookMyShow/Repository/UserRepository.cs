using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public void RemoveUser(User user)
        {
            var userChoosen = _dbContext.Users.FirstOrDefault(u=>u.IdentityUserId == user.IdentityUserId);

                _dbContext.Users.Remove(userChoosen);
                _dbContext.SaveChanges();
        }
    }
}
