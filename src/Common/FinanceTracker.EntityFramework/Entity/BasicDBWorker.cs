using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FinanceTracker.EntityFramework.Entity
{
    public class BasicDBWorker<T, R> where T : class where R : class
    {
        private readonly PostgreSqlContext context;
        private readonly ILogger<R> logger;

        public BasicDBWorker(ILogger<R> logger, PostgreSqlContext context) 
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task AddAsync(T data)
        {
            await context.AddAsync(data);
            var result = await context.SaveChangesAsync();
        }
    }
}
