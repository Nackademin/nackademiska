using System.Collections.Generic;
using Nackademiska.Models;

namespace Nackademiska.Services
{
    public interface IAdminRepository
    {
        void Create(Admin  admin);
        bool Login(string email, string password);
        Admin GetByEmail(string email);

    }
}