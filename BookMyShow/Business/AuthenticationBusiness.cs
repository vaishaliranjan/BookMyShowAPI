using BookMyShow.Models;
using BookMyShow.Models.Enum;
using BookMyShow.Models.ViewsModel;
using BookMyShow.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyShow.Business
{
    public class AuthenticationBusiness:IAuthenticationBusiness
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserRepository _userRepository;
        public AuthenticationBusiness(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
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
                await _userRepository.AddUser(user);
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
                await _userRepository.AddUser(user);
                return true;

            }
            return false;
        }
       public async Task<int> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    var users = await _userRepository.GetAllUsers();
                    var userDetail = users.FirstOrDefault(u => u.IdentityUserId == user.Id);

                    return (int)userDetail.Role;
                }
            }

            return 0;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
