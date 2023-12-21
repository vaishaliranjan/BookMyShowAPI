using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Models.Enum;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Business
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private AppDbContext _appDbContext;
        public CustomerBusiness(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public User GetCustomer(string id)
        {
            var customer = _appDbContext.Users.Find(id);
            if (customer.Role == Role.Customer)
            {
                return customer;
            }
            return null;
        }

        public List<User> GetAllCustomers()
        {
            var users = _appDbContext.Users;
            var customers = users.Where(u => u.Role == Role.Customer).ToList();
            return customers;
        }
    }
}
