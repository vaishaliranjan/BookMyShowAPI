using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Models.Enum;
using BookMyShow.Models.ViewsModel;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookMyShow.Business
{
    public class AuthenticationBusiness:IAuthenticationBusiness
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _appDbContext;
        public AuthenticationBusiness(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
        }
        public async Task<bool> AddUser(AddUserModel model)
        {
            var identityUser = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(identityUser, model.Password);

            Role role;
            if (model.RoleId == "1")
            {
                role = Role.Admin;
                await _userManager.AddToRoleAsync(identityUser, "Admin");
            }
            else if (model.RoleId == "2")
            {
                role = Role.Organizer;
                await _userManager.AddToRoleAsync(identityUser, "Organizer");
            }
            else if (model.RoleId == "3")
            {
                role = Role.Customer;
                await _userManager.AddToRoleAsync(identityUser, "Customer");
            }

            else
            {
                return false;
            }
            if (result.Succeeded)
            {
                var user = new User()
                {
                    IdentityUserId = identityUser.Id,
                    Email = model.Email,
                    Role = role,
                    Username = model.Username,
                    Name = model.Name
                };
                _appDbContext.Users.Add(user);
                _appDbContext.SaveChanges();
                return true;

            }
            return false;
        }
        public async Task<bool> Register( RegisterModel model )
        {
            var identityUser = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(identityUser,model.Password);

            Role role= Role.Customer;
            await _userManager.AddToRoleAsync(identityUser, "Customer");
            
            if (result.Succeeded)
            {
                var user = new User()
                {
                    IdentityUserId = identityUser.Id,
                    Email=model.Email,
                    Role=role,
                    Username=model.Username,
                    Name =model.Name
                }; 
                _appDbContext.Users.Add(user);
                _appDbContext.SaveChanges();
                return true;

            }
            return false;
        }
       public async Task<bool> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username,model.Password, false, false);
            if(result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Logout()
        {
            await _signInManager.SignOutAsync();
            return true;
        }
    }
}
