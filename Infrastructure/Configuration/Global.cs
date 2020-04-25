using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Classes;

namespace Infrastructure.Configuration
{
    public static class Global
    {
        public static List<User> Users => new List<User>()
        {
            new User { Username = "test", Password = "test",Role=Role.Admin },
            new User { Username = "test2", Password = "test2",Role=Role.Manager },
        };
    }
}