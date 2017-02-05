using System.Collections.Generic;
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
    }
}
