using BookMyShow.Models.ViewsModel;
using System.Threading.Tasks;

namespace BookMyShow.Business
{
    public interface IAuthenticationBusiness
    {
        Task<bool> AddUser(AddUserModel model);
        Task<bool> Register(RegisterModel model);
        Task<bool> Login(LoginModel loginModel);
        Task Logout();
    }
}
