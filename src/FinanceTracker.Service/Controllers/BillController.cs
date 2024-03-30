using AutoMapper;
using FinanceTracker.Common.Responses;
using FinanceTracker.EntityFramework.Data;
using FinanceTracker.EntityFramework.Entity;
using FinanceTracker.Service.Models.BillModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinanceTracker.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly ILogger<BillController> logger;
        private readonly IMapper mapper;
        private readonly IBillWorker<Bill> billWorker;

        public BillController(ILogger<BillController> logger, IMapper mapper, IBillWorker<Bill> billWorker)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.billWorker = billWorker;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), (int)StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync(BillModel bill)
        {
            logger.LogInformation("start adding bill...");
            logger.LogInformation($"add data is: {JsonConvert.SerializeObject(bill)}");


            Bill data = mapper.Map<Bill>(bill);

            await billWorker.AddAsync(data);

            logger.LogInformation("complete adding bill...");
            return Created();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IActionResult), (int)StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int id, BillModel bill)
        {
            Bill data = mapper.Map<Bill>(bill);
            data.Id = id;

            await billWorker.UpdateAsync(data);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IActionResult), (int)StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var data = await billWorker.GetByIdAsync(id);

            await billWorker.RemoveAsync(data);
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IActionResult), (int)StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadResponse), (int)StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var data = await billWorker.GetByIdAsync(id);

            var value = mapper.Map<BillDetailModel>(data);

            return Ok(value);
        }
    }
}
