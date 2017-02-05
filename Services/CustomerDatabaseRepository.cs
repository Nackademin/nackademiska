using System;
using System.Linq;
using System.Collections.Generic;
using AddressBook.Data;
using Nackademiska.Models;
using Microsoft.AspNetCore.Identity;

namespace Nackademiska.Services
{
    public class CustomerDatabaseRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerDatabaseRepository(ApplicationDbContext dbContext,
                                          UserManager<ApplicationUser> userManager)
        {
                _dbContext = dbContext;
        }
        public async void Create(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            var user = new ApplicationUser { UserName = customer.Email, 
                                             Email = customer.Email,
                                             CustomerId = customer.Id,
                                             FirstName = customer.FirstName,
                                             LastName = customer.LastName,
                                             Address = customer.Address,
                                             City = customer.City,
                                             PhoneNumber = customer.Phone };
            await _userManager.CreateAsync(user, customer.Password);
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
        // public bool Login(string email, string password)
        // {
        //     return (_dbContext.Customers.FirstOrDefault(c => c.Email == email && c.Password == password) != null);
        // }
    }
}