using Microsoft.AspNetCore.Mvc;
using DeveloperTest.Business.Interfaces;
using DeveloperTest.Models;

namespace DeveloperTest.Controllers
{
    [ApiController, Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(customerService.GetCustomers());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = customerService.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Create(BaseCustomerModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return BadRequest("Name is required");
            }

            if (model.Name.Length < 5)
            {
                return BadRequest("Name must be at least 5 characters");
            }

            if (model.Type != "Large" && model.Type != "Small")
            {
                return BadRequest("Type must be 'Large' or 'Small'");
            }

            var customer = customerService.CreateCustomer(model);

            return Created($"customer/{customer.CustomerId}", customer);
        }
    }
}