using System;
using System.Linq;
using System.Collections.Generic;
using AddressBook.Data;
using Nackademiska.Models;

namespace Nackademiska.Services
{
    public class CustomerDatabaseRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CustomerDatabaseRepository(ApplicationDbContext dbContext)
        {
                _dbContext = dbContext;
        }
        public void Create(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }

        public Customer Get(int id)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Customer> GetAll()
        {
            return _dbContext.Customers.ToList();
        }

        public Customer GetByEmail(string email)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.Email == email);
        }

        public bool Login(string email, string password)
        {
            return (_dbContext.Customers.FirstOrDefault(c => c.Email == email && c.Password == password) != null);
        }
    }
}