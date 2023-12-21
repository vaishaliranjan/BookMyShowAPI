using BookMyShow.Models;
using System.Collections.Generic;

namespace BookMyShow.Business.BusinessInterfaces
{
    public interface ICustomerBusiness
    {
        public List<User> GetAllCustomers();
        public User GetCustomer(string id);
    }
}
