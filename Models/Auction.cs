
using System;

namespace Nackademiska.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public decimal BuyNowPrice { get; set; }
        public bool Sold { get;set; }
    }
}

