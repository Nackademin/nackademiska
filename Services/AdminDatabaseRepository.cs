using System.Linq;
using AddressBook.Data;
using Nackademiska.Models;

namespace Nackademiska.Services
{
    public class AdminDatabaseRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AdminDatabaseRepository(ApplicationDbContext dbContext)
        {
                _dbContext = dbContext;
        }
        public void Create(Admin admin)
        {
            _dbContext.Admins.Add(admin);
            _dbContext.SaveChanges();
        }

        public Admin GetByEmail(string email)
        {
            return _dbContext.Admins.FirstOrDefault(c => c.Email == email);
        }

        public bool Login(string email, string password)
        {
            return (_dbContext.Admins.FirstOrDefault(c => c.Email == email && c.Password == password) != null);
        }
    }
}