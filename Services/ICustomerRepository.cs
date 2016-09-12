using System.Collections.Generic;
using Nackademiska.Models;

namespace Nackademiska.Services
{
    public interface ICustomerRepository
    {
        void Create(Customer  customer);
        ICollection<Customer> GetAll();
        Customer Get(int id);
        Customer GetByEmail(string email);
        bool Login(string email, string password);
    }
}