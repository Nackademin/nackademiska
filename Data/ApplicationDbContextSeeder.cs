using System;
using Nackademiska.Models;
using Nackademiska.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AddressBook.Data
{
    public class ApplicationDbContextSeeder
    {
        private readonly IAuctionRepository _auctions;
        private readonly ICustomerRepository _customers;
        private readonly ISupplierRepository _suppliers;
        private readonly IAdminRepository _admins;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ApplicationDbContextSeeder (IAuctionRepository auctions, 
                                            ICustomerRepository customers, 
                                            ISupplierRepository suppliers,
                                            IAdminRepository admins,
                                            UserManager<ApplicationUser> userManager, 
                                            RoleManager<IdentityRole> roleManager,
                                            SignInManager<ApplicationUser> signInManager)
        {
            _auctions = auctions;
            _customers = customers;
            _suppliers = suppliers;
            _admins = admins;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async void SeedData() 
        {
            var user = await _userManager.FindByNameAsync("sven@svensson.se");
            if (user == null)
            {
                user = new ApplicationUser { UserName = "sven@svensson.se", 
                                                Email = "sven@svensson.se",
                                                CustomerId = 1,
                                                FirstName = "Sven",
                                                LastName = "Svensson",
                                                Address = "Storgatan 1",
                                                City = "Storstan",
                                                PhoneNumber = "081-122211" };
                await _userManager.CreateAsync(user, "sven123456");
            }

            user = await _userManager.FindByNameAsync("anna@andersson.se");
            if (user == null)
            {
                user = new ApplicationUser { UserName = "anna@andersson.se", 
                                                Email = "anna@andersson.se",
                                                CustomerId = 2,
                                                FirstName = "Anna",
                                                LastName = "Andersson",
                                                Address = "Kungsgatan 2",
                                                City = "Orten",
                                                PhoneNumber = "021-178823" };
                await _userManager.CreateAsync(user, "anna123456");
            }
            user = await _userManager.FindByNameAsync("admin@admin.se");
            if (user == null)
            {
                user = new ApplicationUser { UserName = "admin@admin.se", Email = "admin@admin.se"};
                await _userManager.CreateAsync(user, "admin123456");

                var adminRole = new IdentityRole("Admin");
                await _roleManager.CreateAsync(adminRole);
                await _userManager.AddToRoleAsync(user, adminRole.Name);
            }

            if(_suppliers.GetAll().Count == 0)
            {
                _admins.Create(new Admin() { Email = "admin@admin.se",
                                                    Password = "admin12345" });                
                _customers.Create(new Customer() { FirstName = "Sven", 
                                                    LastName = "Svensson", 
                                                    Address = "Storgatan 1", 
                                                    PostalCode = "111 22", 
                                                    City = "Storstan", 
                                                    Phone = "1112233" });
                _customers.Create(new Customer() { FirstName = "Anna", 
                                                    LastName = "Andersson", 
                                                    Address = "Kungagatan 11", 
                                                    PostalCode = "222 33", 
                                                    City = "Lillstan", 
                                                    Phone = "22331112" });                   
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
                                                        StartTime = new DateTime(2017,1,1,10,0,0),
                                                        EndTime =  new DateTime(2017,3,1,10,0,0),
                                                        ImageUrl = "https://thumbs.dreamstime.com/z/antik-stol-som-stoppas-i-sammet-32935346.jpg",
                                                        CategoryId = 1,
                                                        SupplierId = 2,
                                                        BuyNowPrice = 8000M, 
                                                        Sold = false });
                //auctions.CreateBid(new Bid() { AuctionId = 1, CustomerId = 1, DateTime = new DateTime(2016,9,12,10,0,0), BidPrice = 1000M} );
                //_auctions.CreateBid(new Bid() { AuctionId = 1, CustomerId = 2, DateTime = new DateTime(2016,9,12,11,0,0), BidPrice = 2000M} );
                _auctions.CreateAuction(new Auction() { Name="Antikt bord", 
                                                        Description = "En fint antikt bord",
                                                        StartTime = new DateTime(2016,12,10,10,0,0),
                                                        EndTime =  new DateTime(2017,1,10,10,0,0),
                                                        ImageUrl = "http://precisensan.com/antikforum/attachment.php?attachmentid=78277&stc=1&d=1292057973",
                                                        CategoryId = 1,
                                                        SupplierId = 1,
                                                        BuyNowPrice = 18000M,
                                                        Sold = false });
                //_auctions.CreateBid(new Bid() { AuctionId = 2, CustomerId = 1, DateTime = new DateTime(2016,9,11,10,0,0), BidPrice = 5000M} );

                _auctions.CreateCategory(new Category() { Name = "Bilar" });
                _auctions.CreateAuction(new Auction() { Name="Gammal fin bil", 
                                                        Description = "En fin bil",
                                                        StartTime = new DateTime(2017,2,1,10,0,0),
                                                        EndTime =  new DateTime(2017,3,1,10,0,0),
                                                        ImageUrl = "https://lauritzblogsweden.files.wordpress.com/2012/10/bil.png",
                                                        CategoryId = 2,
                                                        SupplierId = 2,
                                                        BuyNowPrice = 1200000M,
                                                        Sold = false });
                //_auctions.CreateBid(new Bid() { AuctionId = 3, CustomerId = 1, DateTime = new DateTime(2016,8,12,10,0,0), BidPrice = 800000M} );
                //_auctions.CreateBid(new Bid() { AuctionId = 3, CustomerId = 2, DateTime = new DateTime(2016,9,11,11,0,0), BidPrice = 1000000M} );                
                _auctions.CreateCategory(new Category() { Name = "Kläder" });
                _auctions.CreateAuction(new Auction() { Name="Vit kostym", 
                                                        Description = "En vit kostym från kungen",
                                                        StartTime = new DateTime(2017,2,10,10,0,0),
                                                        EndTime =  new DateTime(2017,3,22,10,0,0),
                                                        ImageUrl = "https://www.zootsuitstore.com/Shopping/assets/thumbnails/whitesuitsm1.jpg",
                                                        CategoryId = 3,
                                                        SupplierId = 1,
                                                        BuyNowPrice = 5000M,
                                                        Sold = false });                
                _auctions.CreateCategory(new Category() { Name = "Hushåll" });
                _auctions.CreateCategory(new Category() { Name = "Smycken" });
                _auctions.CreateAuction(new Auction() { Name="Special smycke", 
                                                        Description = "En smycke från antiken",
                                                        StartTime = new DateTime(2017,1,1,10,0,0),
                                                        EndTime =  new DateTime(2017,2,15,10,0,0),
                                                        ImageUrl = "https://b2bmarketplaces.files.wordpress.com/2013/08/vintage-antique-jewelry1.jpg",
                                                        CategoryId = 5,
                                                        SupplierId = 1,
                                                        BuyNowPrice = 35000M,
                                                        Sold = false });    
                //_auctions.CreateBid(new Bid() { AuctionId = 5, CustomerId = 1, DateTime = new DateTime(2016,8,12,9,0,0), BidPrice = 1000M} );                                                                     
            }
        }
    }

}