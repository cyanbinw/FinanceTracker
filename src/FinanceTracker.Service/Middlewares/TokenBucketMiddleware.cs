using FinanceTracker.Common.Request;
using Microsoft.AspNetCore.RateLimiting;
using System.Collections.Concurrent;
using System.Threading.RateLimiting;

namespace FinanceTracker.Service.Middlewares
{
    public class TokenBucketMiddleware
    {
        private readonly RequestDelegate _next;

        readonly static ConcurrentDictionary<string, TokenBucketRateLimiter> pathTokenBucket = new ConcurrentDictionary<string, TokenBucketRateLimiter>();


        public TokenBucketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;
            
            if (!string.IsNullOrEmpty(path))
            {

                var limiter = pathTokenBucket.GetOrAdd(path, _ => CreateLimiter());

                using var lease = await limiter.AcquireAsync(permitCount: 1);
                if (!lease.IsAcquired)
                {
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.Response.Headers["Retry-After"] = "1";
                    await context.Response.WriteAsync("Too many requests.");
                    return;
                }
            }
            await _next(context);
        }

        private TokenBucketRateLimiter CreateLimiter()
        {
            return new TokenBucketRateLimiter(
                new TokenBucketRateLimiterOptions
                {
                    TokenLimit = 10,
                    TokensPerPeriod = 1,
                    ReplenishmentPeriod = TimeSpan.FromSeconds(1),
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 0,
                    AutoReplenishment = true
                });
        }
    }
}
