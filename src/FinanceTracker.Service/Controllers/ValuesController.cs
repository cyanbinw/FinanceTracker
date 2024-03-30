using FinanceTracker.EntityFramework.Data;
using FinanceTracker.EntityFramework.Entity;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> logger;
        private readonly IBillWorker<Bill> billWorker;

        public ValuesController(ILogger<ValuesController> logger, IBillWorker<Bill> billWorker)
        {
            this.logger = logger;
            this.billWorker = billWorker;
        }

        [HttpGet]
        public async Task<IActionResult> TaskAsync()
        {
            logger.LogInformation("test");
            var bill = new Bill
            {
                BillName = "test",
                BillNumber = "asdfjlaskdfj",
                Date = DateTime.Now,
                Account = 1234,
                Type = "asdf"
            };
            await billWorker.AddAsync(bill);
            return Ok();
        }
    }
}
