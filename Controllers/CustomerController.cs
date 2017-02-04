using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Nackademiska.Services;
using Nackademiska.Models;

namespace Nackademiska.Controllers
{
    // [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customers;
        public CustomerController (ICustomerRepository customers)
        {
            _customers = customers;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _customers.GetAll();
        }

        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _customers.Get(id);
        }

        [HttpPost]
        public ActionResult Post([FromBody]Customer customer)
        {
            try
            {
                customer.Id = 0;
                _customers.Create(customer);
                return Ok();
            }
            catch (Exception)
            {
               return BadRequest();
            }
        }

        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
