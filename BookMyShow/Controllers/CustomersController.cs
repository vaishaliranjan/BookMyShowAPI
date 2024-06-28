using BookMyShow.Business;
using BookMyShow.Business.BusinessInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookMyShow
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerBusiness _customerBusiness;
        public CustomersController(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                if (id == null)
                {
                    var customers = await _customerBusiness.GetAllCustomers();
                    if(customers == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Customers not found");
                    }
                    return StatusCode(StatusCodes.Status200OK, customers);
                }
                var customer = _customerBusiness.GetCustomer(id);
                if (customer == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Customer not found");
                }
                return StatusCode(StatusCodes.Status200OK,customer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _customerBusiness.DeleteCustomer(id);
                if (result)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                return StatusCode(StatusCodes.Status404NotFound, "Customer not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
