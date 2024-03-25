using FinanceTracker.EntityFramework.Data;
using Microsoft.Extensions.Logging;

namespace FinanceTracker.EntityFramework.Entity
{
    public class BillWorker : BasicDBWorker<Bill, BillWorker>, IBillWorker
    {

        public BillWorker(ILogger<BillWorker> logger, PostgreSqlContext context) : base(logger, context)
        {

        }
    }
}
