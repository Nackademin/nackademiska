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
    public class CategoryController : Controller
    {
        private readonly IAuctionRepository _auctions;

        public CategoryController (IAuctionRepository auctions)
        {
            _auctions = auctions;
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _auctions.GetAllCategories();
        }

        // [HttpGet("{id}")]
        // public Auction Get(int id)
        // {
        //     return _auctions.GetAuction(id);
        // }

        // [HttpPost]
        // public void Post([FromBody]Auction value)
        // {
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
