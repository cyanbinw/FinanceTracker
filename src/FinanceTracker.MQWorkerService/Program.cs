using FinanceTracker.MQWorkerService.Consumers;
using MassTransit;

namespace FinanceTracker.MQWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<SubmitOrderConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://localhost", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("order-created-queue", e =>
                    {
                        e.ConfigureConsumer<SubmitOrderConsumer>(context);
                        e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5))); // ÷ÿ ‘ª˙÷∆
                        e.SetQueueArgument("x-dead-letter-exchange", "dlx_exchange");
                        e.SetQueueArgument("x-dead-letter-routing-key", "dlq_queue");
                        e.SetQueueArgument("x-message-ttl", 60000);
                        e.UseNewtonsoftJsonSerializer();
                        e.UseNewtonsoftJsonDeserializer();
                    });
                });
            });

            var host = builder.Build();
            host.Run();
        }
    }
}