using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.DataPersistenceLayer.Entity
{
    public interface IBasicDBWorker<T> where T : class
    {
        public Task AddAsync(T data);
    }
}
