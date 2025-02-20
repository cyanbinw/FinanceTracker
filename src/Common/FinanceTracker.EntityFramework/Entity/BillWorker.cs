using FinanceTracker.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinanceTracker.EntityFramework.Entity
{
    public class BillWorker : BaseDBWorker<Bill, BillWorker>, IBillWorker
    {

        public BillWorker(ILogger<BillWorker> logger, PostgreSqlContext context) : base(logger, context)
        {

        }

        public override async Task<List<Bill>> GetAllAsync()
        {
            return await context.Bills.ToListAsync() ?? new List<Bill>();
        }

    }
}
