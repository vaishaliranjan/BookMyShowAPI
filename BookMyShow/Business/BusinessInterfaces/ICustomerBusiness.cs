using BookMyShow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyShow.Business.BusinessInterfaces
{
    public interface ICustomerBusiness
    {
        public Task<List<User>> GetAllCustomers();
        public Task<User> GetCustomer(string id);
    }
}
