using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Nackademiska.Services;
using Nackademiska.Models;
using Microsoft.AspNetCore.Cors;

namespace Nackademiska.Controllers
{
    [EnableCors("CorsPolicy")]
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

        // [HttpPost]
        // public void Post([FromBody]Customer customer)
        // {
        //     _customers.Create(customer);
        // }

        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
