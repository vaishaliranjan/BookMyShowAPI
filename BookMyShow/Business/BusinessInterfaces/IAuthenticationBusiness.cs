using BookMyShow.Models.ViewsModel;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookMyShow.Business
{
    public interface IAuthenticationBusiness
    {
        Task<bool> Register(IdentityUser identityUser, RegisterModel model);
        Task<bool> Login(LoginModel loginModel);
        Task<bool> Logout();
    }
}
