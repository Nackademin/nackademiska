using System;
using System.Collections.Generic;
using AddressBook.Data;
using Nackademiska.Models;
using System.Linq;

namespace Nackademiska.Services
{
    public class AuctionDatabaseRepository : IAuctionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AuctionDatabaseRepository(ApplicationDbContext dbContext)
        {
                _dbContext = dbContext;
        }
        public void BuyNow(int auctionId, int customerId)
        {
            var auction = _dbContext.Auctions.FirstOrDefault(a => a.Id == auctionId);
            auction.Sold = true;
            _dbContext.Auctions.Update(auction);
            _dbContext.SaveChanges();
        }
        public void CreateAuction(Auction auction)
        {
            _dbContext.Auctions.Add(auction);
            _dbContext.SaveChanges();
        }
        public bool CreateBid(BidInformation bidInformation)
        {
            if (_dbContext.Bids.Where(b => b.AuctionId == bidInformation.AuctionId).Max(x => x.BidPrice) >= bidInformation.BidPrice)
                return false;
            var bid = new Bid() { AuctionId = bidInformation.AuctionId,
                                    CustomerId = bidInformation.CustomerId,
                                    DateTime = DateTime.Now,
                                    BidPrice = bidInformation.BidPrice };
            _dbContext.Bids.Add(bid);
            _dbContext.SaveChanges();
            return true;
        }
        public void CreateCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }
        public ICollection<Auction> GetAllAuctions()
        {
            return _dbContext.Auctions.ToList();
        }
        public ICollection<Bid> GetAllBids(int id)
        {
            return _dbContext.Bids.Where(b => b.AuctionId == id).ToList();
        }
        public ICollection<Category> GetAllCategories()
        {
            return _dbContext.Categories.ToList();
        }
        public ICollection<Auction> GetAllCompletedAuctions()
        {
            return _dbContext.Auctions.Where(a => a.Sold == true || (a.EndTime < DateTime.Now && a.Bids.Any(c => a.Id == c.AuctionId))).ToList();
        }
        public Auction GetAuction(int id)
        {
            return _dbContext.Auctions.FirstOrDefault(a => a.Id == id);
        }
        public Category GetCategory(int id)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.Id == id);
        }
    }
}