using FinanceTracker.BillDomain;
using FinanceTracker.BillDomain.Models.BillModels;
using FinanceTracker.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinanceTracker.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly ILogger<BillController> logger;
        private readonly IBillRepository billRepository;

        public BillController(ILogger<BillController> logger, IBillRepository billRepository)
        {
            this.logger = logger;
            this.billRepository = billRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BillDetailModel), (int)StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync(BillModel bill)
        {
            logger.LogInformation("start adding bill...");
            logger.LogInformation($"add data is: {JsonConvert.SerializeObject(bill)}");

            var result = await billRepository.CreateBillAsync(bill);

            logger.LogInformation("complete adding bill...");
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BillDetailModel), (int)StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int id, BillModel bill)
        {
            BillDetailModel data = new BillDetailModel(id, bill);
            data.Id = id;

            await billRepository.UpdateBillAsync(data);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IActionResult), (int)StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var data = await billRepository.GetBillByIdAsync(id);

            await billRepository.DeleteBillAsync(id);
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BillDetailModel), (int)StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var data = await billRepository.GetBillByIdAsync(id);

            return Ok(data);
        }
    }
}
