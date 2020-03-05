using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Models;
using DataAccessLayer.Extensions;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : Repository<User>
    {
        private DbContext _dbContext;

        public UserRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<User> GetUsers()
        {
            using (var command = _dbContext.CreateCommand())
            {
                command.CommandText = "exec [dbo].[sp_GetUsers]";

                return this.ToList(command).ToList();
            }
        }

        public User CreateUser(User user)
        {
            using (var command = _dbContext.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_CreateUser";
                command.Parameters.Add(command.CreateParameter("@FirstName", user.FirstName));
                command.Parameters.Add(command.CreateParameter("@LastName", user.FirstName));
                command.Parameters.Add(command.CreateParameter("@UserName", user.FirstName));
                command.Parameters.Add(command.CreateParameter("@Password", user.FirstName));
                command.Parameters.Add(command.CreateParameter("@Email", user.FirstName));

                return this.ToList(command).FirstOrDefault();
            }
        }

    }
}
