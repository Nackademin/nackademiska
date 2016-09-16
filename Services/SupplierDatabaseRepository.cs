using System;
using System.Linq;
using System.Collections.Generic;
using AddressBook.Data;
using Nackademiska.Models;

namespace Nackademiska.Services
{
    public class SupplierDatabaseRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public SupplierDatabaseRepository(ApplicationDbContext dbContext)
        {
                _dbContext = dbContext;
        }
        public void Create(Supplier supplier)
        {
            _dbContext.Suppliers.Add(supplier);
            _dbContext.SaveChanges();
        }

        public Supplier Get(int id)
        {
            return _dbContext.Suppliers.FirstOrDefault(s => s.Id == id);
        }

        public ICollection<Supplier> GetAll()
        {
            return _dbContext.Suppliers.ToList();
        }
    }
}