using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Services
{
    public interface IUserService
    {
        IList<User> GetUsers();

        User CreateUser(User user);


    }
}
