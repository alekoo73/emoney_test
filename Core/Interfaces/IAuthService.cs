using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Core.Interfaces
{
    public interface IAuthService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}
