using Microsoft.AspNetCore.Identity;

namespace BookMyShow.Business
{
    public class AccountBusiness:IAccountBusiness
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AccountBusiness(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

    }
}
