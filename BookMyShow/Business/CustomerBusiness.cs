using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using BookMyShow.Models.Enum;
using BookMyShow.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Business
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly IUserRepository userRepository;
        public CustomerBusiness(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User GetCustomer(string id)
        {
            var customers = GetAllCustomers();
            var customer = customers.FirstOrDefault(o => o.IdentityUserId.Equals(id));
            if (customer == null)
            {
                return null;
            }
            return customer;
        }

        public List<User> GetAllCustomers()
        {
            var users = userRepository.GetAllUsers();
            var customers = users.Where(u => u.Role == Role.Customer).ToList();
            return customers;
        }
    }
}
