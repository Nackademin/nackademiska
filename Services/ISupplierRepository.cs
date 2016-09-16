using System.Collections.Generic;
using Nackademiska.Models;

namespace Nackademiska.Services
{
    public interface ISupplierRepository
    {
        void Create(Supplier supplier);
        ICollection<Supplier> GetAll();
        Supplier Get(int id);

    }
}