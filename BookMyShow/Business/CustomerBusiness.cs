using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using BookMyShow.Models.Enum;
using BookMyShow.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyShow.Business
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly IUserRepository userRepository;
        public CustomerBusiness(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> GetCustomer(string id)
        {
            var customers =await GetAllCustomers();
            var customer = customers.FirstOrDefault(o => o.IdentityUserId.Equals(id));
            if (customer == null)
            {
                return null;
            }
            return customer;
        }

        public async Task<List<User>> GetAllCustomers()
        {
            var users = await userRepository.GetAllUsers();
            var customers = users.Where(u => u.Role == Role.Customer).ToList();
            return customers;
        }

        public async Task<bool> DeleteCustomer(string id)
        {
            var admin = await GetCustomer(id);
            if (admin == null)
            {
                return false;
            }
            await userRepository.RemoveUser(admin);
            return true;
        }

      
    }
}
