using FinanceTracker.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> logger;
        private readonly PostgreSqlContext dbContext;

        public ValuesController(ILogger<ValuesController> logger, PostgreSqlContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpPost("Migrate")]
        public async Task<IActionResult> TaskAsync()
        {
            logger.LogInformation("test");
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }
            return Ok();
        }
    }
}
