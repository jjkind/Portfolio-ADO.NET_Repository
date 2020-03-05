using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer
{
    public abstract class Repository<TEntity> where TEntity : new()
    {
        DbContext _dbContext;

        public Repository(DbContext context)
        {
            _dbContext = context;
        }

        protected DbContext Context
        {
            get { return this._dbContext; }
        }

        protected IEnumerable<TEntity> ToList(IDbCommand command)
        {
            using (var record = command.ExecuteReader())
            {
                List<TEntity> items = new List<TEntity>();
                while (record.Read())
                {
                    items.Add(Map<TEntity>(record));
                }

                return items;
            }
        }


        protected TEntity Map<T>(IDataRecord record)
        {
            var objT = Activator.CreateInstance<TEntity>();
            foreach (var property in typeof(T).GetProperties())
            {
                if (!record.IsDBNull(record.GetOrdinal(property.Name)))
                    property.SetValue(objT, record[property.Name]);
            }

            return objT;

        }
    }
}
