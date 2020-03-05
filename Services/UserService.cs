using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class UserService : IUserService
    {

        private IConnectionFactory connectionFactory;

        public User CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
