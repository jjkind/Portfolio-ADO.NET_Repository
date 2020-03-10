using DataAccessLayer;
using DataAccessLayer.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class UserService : IUserService
    {
        private IConnectionFactory _connectionFactory;

        public UserService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        private IConnectionFactory connectionFactory;

        public User CreateUser(User user)
        {
            var context = new DbContext(_connectionFactory);

            var userRepository = new UserRepository(context);

            return userRepository.CreateUser(user);
        }

        public IList<User> GetUsers()
        {
            var context = new DbContext(_connectionFactory);

            var userRepository = new UserRepository(context);

            return userRepository.GetUsers();
        }
    }
}
