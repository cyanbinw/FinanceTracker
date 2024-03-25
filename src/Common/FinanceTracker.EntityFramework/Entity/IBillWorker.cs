using FinanceTracker.EntityFramework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.EntityFramework.Entity
{
    public interface IBillWorker
    {
        public Task AddAsync(Bill data);
    }
}
