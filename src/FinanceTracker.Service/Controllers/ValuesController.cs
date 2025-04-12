using FinanceTracker.EntityFramework;
using FinanceTracker.MQWorkerService.Models;
using MassTransit;
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
        private readonly IPublishEndpoint publishEndpoint;

        public ValuesController(ILogger<ValuesController> logger, PostgreSqlContext dbContext, IPublishEndpoint publishEndpoint)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.publishEndpoint = publishEndpoint;
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

        [HttpPost("run")]
        public ActionResult Run()
        {
            var order = new SubmitOrder
            {
                OrderId = Guid.NewGuid(),
                CustomerName = "Alice",
                Amount = 100.50m
            };
            publishEndpoint.Publish<SubmitOrder>(order);

            return Ok();
        }

        [HttpGet("test")]
        public ActionResult Test()
        {
            Task.Run(() =>
            {
                Console.WriteLine("== 子线程执行开始 ==");
                Thread.Sleep(1000);
                Console.WriteLine("== 子线程执行结束 ==");
            });
            return Ok();
        }
    }
}
