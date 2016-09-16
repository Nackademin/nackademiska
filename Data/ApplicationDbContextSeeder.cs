using System;
using System.Threading.Tasks;
using Nackademiska.Models;
using Nackademiska.Services;

namespace AddressBook.Data
{
    public class ApplicationDbContextSeeder
    {
        private readonly IAuctionRepository _auctions;
        private readonly ICustomerRepository _customers;
        private readonly ISupplierRepository _suppliers;
        public ApplicationDbContextSeeder (IAuctionRepository auctions, 
                                            ICustomerRepository customers, 
                                            ISupplierRepository suppliers)
        {
            _auctions = auctions;
            _customers = customers;
            _suppliers = suppliers;
        }

        public void SeedData() 
        {
            if(_customers.GetAll().Count == 0)
            {
                _customers.Create(new Customer() { FirstName = "Sven", 
                                                    LastName = "Svensson", 
                                                    Address = "Storgatan 1", 
                                                    PostalCode = "111 22", 
                                                    City = "Storstan", 
                                                    Phone = "1112233",
                                                    Email = "sven@svensson.se",
                                                    Password = "sven1234" });
                _customers.Create(new Customer() { FirstName = "Anna", 
                                                    LastName = "Andersson", 
                                                    Address = "Kungagatan 11", 
                                                    PostalCode = "222 33", 
                                                    City = "Lillstan", 
                                                    Phone = "22331112",
                                                    Email = "anna@andersson.se",
                                                    Password = "anna1234" });                   
                _suppliers.Create(new Supplier() { Name = "Antiktorget", 
                                                    Address = "Torget 1", 
                                                    PostalCode = "111 22", 
                                                    City = "Storstan", 
                                                    Phone = "67676767",
                                                    Email = "info@antiktorget.se",
                                                    Commission = 0.2M });
                _suppliers.Create(new Supplier() { Name = "Oceanens Antikervariat", 
                                                    Address = "Centrumgatan 12", 
                                                    PostalCode = "333 22", 
                                                    City = "Landsbyggden", 
                                                    Phone = "4323423",
                                                    Email = "info@oceanen.se",
                                                    Commission = 0.1M });
                _auctions.CreateCategory(new Category() { Name = "Möbler" });
                _auctions.CreateAuction(new Auction() { Name="Antik stol", 
                                                        Description = "En fin antik stol från 1800-talet",
                                                        StartTime = new DateTime(2016,9,9,10,0,0),
                                                        EndTime =  new DateTime(2016,9,23,10,0,0),
                                                        ImageUrl = "https://pixabay.com/static/uploads/photo/2015/09/27/17/51/antique-961102_960_720.png",
                                                        CategoryId = 1,
                                                        SupplierId = 2,
                                                        BuyNowPrice = 8000M, 
                                                        Sold = false });
                //auctions.CreateBid(new Bid() { AuctionId = 1, CustomerId = 1, DateTime = new DateTime(2016,9,12,10,0,0), BidPrice = 1000M} );
                //_auctions.CreateBid(new Bid() { AuctionId = 1, CustomerId = 2, DateTime = new DateTime(2016,9,12,11,0,0), BidPrice = 2000M} );
                _auctions.CreateAuction(new Auction() { Name="Antikt bord", 
                                                        Description = "En fint antikt bord",
                                                        StartTime = new DateTime(2016,8,10,10,0,0),
                                                        EndTime =  new DateTime(2016,9,18,10,0,0),
                                                        ImageUrl = "http://precisensan.com/antikforum/attachment.php?attachmentid=78277&stc=1&d=1292057973",
                                                        CategoryId = 1,
                                                        SupplierId = 1,
                                                        BuyNowPrice = 18000M,
                                                        Sold = false });
                //_auctions.CreateBid(new Bid() { AuctionId = 2, CustomerId = 1, DateTime = new DateTime(2016,9,11,10,0,0), BidPrice = 5000M} );

                _auctions.CreateCategory(new Category() { Name = "Bilar" });
                _auctions.CreateAuction(new Auction() { Name="Gammal fin bil", 
                                                        Description = "En fin bil",
                                                        StartTime = new DateTime(2016,8,9,10,0,0),
                                                        EndTime =  new DateTime(2016,12,20,10,0,0),
                                                        ImageUrl = "https://pixabay.com/static/uploads/photo/2012/02/23/10/43/antique-16050_960_720.jpg",
                                                        CategoryId = 2,
                                                        SupplierId = 2,
                                                        BuyNowPrice = 1200000M,
                                                        Sold = false });
                //_auctions.CreateBid(new Bid() { AuctionId = 3, CustomerId = 1, DateTime = new DateTime(2016,8,12,10,0,0), BidPrice = 800000M} );
                //_auctions.CreateBid(new Bid() { AuctionId = 3, CustomerId = 2, DateTime = new DateTime(2016,9,11,11,0,0), BidPrice = 1000000M} );                
                _auctions.CreateCategory(new Category() { Name = "Kläder" });
                _auctions.CreateAuction(new Auction() { Name="Vit kostym", 
                                                        Description = "En vit kostym från kungen",
                                                        StartTime = new DateTime(2016,9,10,10,0,0),
                                                        EndTime =  new DateTime(2016,9,22,10,0,0),
                                                        ImageUrl = "https://pixabay.com/static/uploads/photo/2015/02/14/02/20/son-in-law-636021_960_720.jpg",
                                                        CategoryId = 3,
                                                        SupplierId = 1,
                                                        BuyNowPrice = 5000M,
                                                        Sold = false });                
                _auctions.CreateCategory(new Category() { Name = "Hushåll" });
                _auctions.CreateCategory(new Category() { Name = "Smycken" });
                _auctions.CreateAuction(new Auction() { Name="Special smycke", 
                                                        Description = "En smycke från antiken",
                                                        StartTime = new DateTime(2016,8,10,10,0,0),
                                                        EndTime =  new DateTime(2016,9,15,10,0,0),
                                                        ImageUrl = "https://pixabay.com/static/uploads/photo/2016/02/02/15/54/jewellery-1175533_960_720.jpg",
                                                        CategoryId = 5,
                                                        SupplierId = 1,
                                                        BuyNowPrice = 35000M,
                                                        Sold = false });    
                //_auctions.CreateBid(new Bid() { AuctionId = 5, CustomerId = 1, DateTime = new DateTime(2016,8,12,9,0,0), BidPrice = 1000M} );                                                                     
            }
        }
    }

}