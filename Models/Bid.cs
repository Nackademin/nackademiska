using System;

namespace Nackademiska.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal BidPrice { get; set; }
    }
    public class BidInformation
    {
        public int AuctionId { get; set; }
        public int CustomerId { get; set; }
        public decimal BidPrice { get; set; }
    }
}

