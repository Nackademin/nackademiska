using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Nackademiska.Services;
using Nackademiska.Models;

namespace Nackademiska.Controllers
{
    [Route("api/[controller]")]
    public class AuctionController : Controller
    {
        private readonly IAuctionRepository _auctions;
        private readonly ICustomerRepository _customers;
        private readonly ISupplierRepository _suppliers;
        public AuctionController (IAuctionRepository auctions, 
                                    ICustomerRepository customers, 
                                    ISupplierRepository suppliers)
        {
            _auctions = auctions;
            _customers = customers;
            _suppliers = suppliers;
        }

        [HttpGet]
        public IEnumerable<Auction> Get()
        {
            return _auctions.GetAllAuctions();
        }

        [HttpGet("sold")]
        public IEnumerable<Auction> GetSold()
        {
            return _auctions.GetAllCompletedAuctions();
        }

        [HttpGet("{id}")]
        public Auction Get(int id)
        {
            return _auctions.GetAuction(id);
        }

        [HttpPost("buynow")]
        public ActionResult Post([FromBody]BuyNowInformation buyNowInformation)
        {
            try
            {
                if(buyNowInformation.AuctionId > 0 && buyNowInformation.CustomerId > 0)
                {
                    _auctions.BuyNow(buyNowInformation.AuctionId, buyNowInformation.CustomerId);
                    return Ok();
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

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
