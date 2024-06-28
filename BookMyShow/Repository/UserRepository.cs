using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyShow.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public UserRepository(AppDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
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
            var userToDelete= await _userManager.FindByIdAsync(userChoosen.IdentityUserId);
            await _userManager.DeleteAsync(userToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
