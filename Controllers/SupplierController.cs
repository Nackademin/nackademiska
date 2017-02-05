using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Nackademiska.Services;
using Nackademiska.Models;

namespace Nackademiska.Controllers
{
    [Route("api/[controller]")]
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository _supplier;
        public SupplierController (ISupplierRepository supplier)
        {
            _supplier = supplier;
        }

        [HttpGet]
        public IEnumerable<Supplier> Get()
        {
            return _supplier.GetAll();
        }

        [HttpGet("{id}")]
        public Supplier Get(int id)
        {
            return _supplier.Get(id);
        }
    }
}
