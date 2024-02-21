using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyShow.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task RemoveUser(User user)
        {
            var userChoosen = await _dbContext.Users.FirstOrDefaultAsync(u=>u.IdentityUserId == user.IdentityUserId);
            _dbContext.Users.Remove(userChoosen);
            await _dbContext.SaveChangesAsync();
        }
    }
}
