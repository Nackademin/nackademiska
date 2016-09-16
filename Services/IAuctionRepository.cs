using System.Collections.Generic;
using Nackademiska.Models;

namespace Nackademiska.Services
{
    public interface IAuctionRepository
    {
        void CreateAuction(Auction auction);
        void CreateCategory(Category category);
        void CreateBid(BidInformation bidInformation);
        void BuyNow(int auctionId, int customerId);
        ICollection<Auction> GetAllAuctions();
        ICollection<Auction> GetAllCompletedAuctions();
        ICollection<Category> GetAllCategories();
        ICollection<Bid> GetAllBids(int auctionId);
        Auction GetAuction(int id);
        Category GetCategory(int id);

    }
}