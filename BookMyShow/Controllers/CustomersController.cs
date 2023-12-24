using BookMyShow.Business;
using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookMyShow.Controllers
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
        public IActionResult Get(string id)
        {
            try
            {
                if (id == null)
                {
                    return Ok(_customerBusiness.GetAllCustomers());
                }
                var customer = _customerBusiness.GetCustomer(id);
                if (customer == null)
                {
                    return NotFound("Customer not found!");
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
