using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccessLayer.Extensions
{
    public static class IDbCommandExtension
    {
        public static IDbDataParameter CreateParameter(this IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;

            return parameter;
        }
    }
}
