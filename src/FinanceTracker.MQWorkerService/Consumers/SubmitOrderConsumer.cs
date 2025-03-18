using FinanceTracker.MQWorkerService.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.MQWorkerService.Consumers
{
    internal class SubmitOrderConsumer : IConsumer<SubmitOrder>
    {
        private readonly ILogger<SubmitOrderConsumer> logger;

        public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            logger.LogInformation(context.Message.CustomerName);

            await Task.CompletedTask;
        }
    }
}
