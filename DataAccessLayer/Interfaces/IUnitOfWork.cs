using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public interface IUnitOfWork
    {
        void Dispose();

        void SaveChanges();        
    }
}
