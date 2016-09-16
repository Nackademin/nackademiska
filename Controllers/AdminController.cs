using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nackademiska.Services;
using Nackademiska.Models;

namespace Nackademiska.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _admin;
        private readonly IAuctionRepository _auctions;
        public AdminController (IAdminRepository admin,
                                    IAuctionRepository auctions)
        {
            _admin = admin;
            _auctions = auctions;
        }

        [HttpGet]
        public IEnumerable<Auction> Get()
        {
            return _auctions.GetAllCompletedAuctions();
        }

        // [HttpGet("{id}")]
        // public Customer Get(int id)
        // {
        //     return _customers.Get(id);
        // }

        [HttpPost("login")]
        public ActionResult Post([FromBody]LoginInformation loginInformation)
        {
            try {
                if(_admin.Login(loginInformation.Email, loginInformation.Password)) 
                {
                    return new JsonResult(new { id = _admin.GetByEmail(loginInformation.Email).Id } );
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
