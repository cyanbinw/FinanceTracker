using FinanceTracker.DataPersistenceLayer.Data;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.DataPersistenceLayer.Entity
{
    public class BillWorker : BasicDBWorker<Bill, BillWorker>, IBillWorker
    {

        public BillWorker(ILogger<BillWorker> logger, PostgreSqlContext context) : base(logger, context)
        {

        }
    }
}
