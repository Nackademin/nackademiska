using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Nackademiska.Services;
using Nackademiska.Models;

namespace Nackademiska.Controllers
{
    // [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class BidController : Controller
    {
        private readonly IAuctionRepository _auctions;
        private readonly ICustomerRepository _customers;
        private readonly ISupplierRepository _suppliers;
        public BidController (IAuctionRepository auctions, 
                                    ICustomerRepository customers, 
                                    ISupplierRepository suppliers)
        {
            _auctions = auctions;
            _customers = customers;
            _suppliers = suppliers;
        }

        [HttpGet("{id}")]
        public IEnumerable<Bid> Get(int id)
        {
            return _auctions.GetAllBids(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody]BidInformation bidInformation)
        {
            try
            {
                if ( bidInformation.AuctionId>0 && bidInformation.CustomerId>0 && bidInformation.BidPrice > 0) {
                    if( !_auctions.CreateBid(bidInformation) )
                        return BadRequest();
                    return Ok();
                }
                else {
                    return BadRequest();
                }
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
