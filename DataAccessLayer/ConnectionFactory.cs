using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Configuration;
using Serenity.Data;

namespace DataAccessLayer
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly DbProviderFactory _provider;
        private readonly string _connectionString;
        private readonly string _name;

        public ConnectionFactory(string connectionName)
        {
            if (connectionName == null) throw new ArgumentNullException("connectionName");

            var connString = ConfigurationManager.ConnectionStrings[connectionName];
            if (connString == null)
                throw new ConfigurationErrorsException(string.Format("Failed to find connection string named '{0}' in web.config", connectionName));

            _name = connString.ProviderName;
            _provider = DbProviderFactories.GetFactory(_name);
            _connectionString = connString.ConnectionString;

        }

        public IDbConnection Create()
        {
            var connection = _provider.CreateConnection();
            if (connection == null)
                throw new ConfigurationErrorsException(string.Format("Failed to create connection using the connection string name '{0}' in web.config", _name));

            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }

    }
}
