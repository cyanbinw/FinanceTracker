using FinanceTracker.DataPersistenceLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.DataPersistenceLayer.Entity
{
    public interface IBillWorker
    {
        public Task AddAsync(Bill data);
    }
}
