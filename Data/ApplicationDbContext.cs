using Microsoft.EntityFrameworkCore;
using Nackademiska.Models;

namespace AddressBook.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        DbSet<Auction> Auctions { get; set; }
    }
}