using System;
using Microsoft.AspNetCore.Mvc;
using Nackademiska.Services;
using Nackademiska.Models;
using Microsoft.AspNetCore.Cors;

namespace Nackademiska.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ICustomerRepository _customers;
        private readonly IAdminRepository _admins;
        public AccountController (ICustomerRepository customers,
                                    IAdminRepository admins)
        {
            _customers = customers;
            _admins = admins;
        }

        // [HttpGet]
        // public IEnumerable<Customer> Get()
        // {
        //     return _customers.GetAll();
        // }

        // [HttpGet("{id}")]
        // public Customer Get(int id)
        // {
        //     return _customers.Get(id);
        // }

        [HttpPost("login")]
        public ActionResult Post([FromBody]LoginInformation loginInformation)
        {
            try {
                if(_customers.Login(loginInformation.Email, loginInformation.Password)) 
                {
                    return new JsonResult(new { id = _customers.GetByEmail(loginInformation.Email).Id } );
                }
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest();
            }   
        }

        [HttpPost("admin/login")]
        public ActionResult PostAdmin([FromBody]LoginInformation loginInformation)
        {
            try {
                if(_admins.Login(loginInformation.Email, loginInformation.Password)) 
                {
                    return new JsonResult(new { id = _admins.GetByEmail(loginInformation.Email).Id } );
                }
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest();
            }   
        }

        // [HttpPost]
        // public void Post([FromBody]Customer customer)
        // {
        //     _customers.Create(customer);
        // }

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
