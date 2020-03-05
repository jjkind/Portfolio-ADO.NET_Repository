using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;

namespace DataAccessLayer
{
    public class DbContext
    {
        private readonly IDbConnection _connection;
        private readonly IConnectionFactory _connectionFactory;
        private readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();
        private readonly LinkedList<AdoNetUnitOfWork> _unitOfWork = new LinkedList<AdoNetUnitOfWork>();

        public DbContext(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.Create();
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            var transaction = _connection.BeginTransaction();
            var unitOfWork = new AdoNetUnitOfWork(transaction, RemoveTransaction, RemoveTransaction);

            _rwLock.EnterWriteLock();
            _unitOfWork.AddLast(unitOfWork);
            _rwLock.ExitWriteLock();

            return unitOfWork;
        }

        public IDbCommand CreateCommand()
        {
            var cmd = _connection.CreateCommand();

            _rwLock.EnterReadLock();
            if (_unitOfWork.Count > 0)
                cmd.Transaction = _unitOfWork.First.Value.Transaction;
            _rwLock.ExitReadLock();

            return cmd;
        }

        public void RemoveTransaction(AdoNetUnitOfWork obj)
        {
            _rwLock.EnterWriteLock();
            _unitOfWork.Remove(obj);
            _rwLock.ExitWriteLock();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

    }
}
