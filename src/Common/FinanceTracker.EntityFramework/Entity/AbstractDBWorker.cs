using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.EntityFramework.Entity
{
    public abstract class AbstractDBWorker<T>
    {
        public abstract Task AddAsync(T data);

        public abstract Task UpdateAsync(T data);

        public abstract Task<T> GetByIdAsync(int id);

        // must override
        public abstract Task<List<T>> GetAllAsync();

        public abstract Task RemoveAsync(T data);
    }
}
