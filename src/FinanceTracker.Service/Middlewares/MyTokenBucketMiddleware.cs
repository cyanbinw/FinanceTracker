using FinanceTracker.Common.Request;
using System.Collections.Concurrent;

namespace FinanceTracker.Service.Middlewares
{
    /// <summary>
    /// my own implemented of a TokenBucket
    /// </summary>
    public class MyTokenBucketMiddleware
    {
        private readonly RequestDelegate _next;

        readonly static ConcurrentDictionary<string, TokenBucket> pathTokenBucket = new ConcurrentDictionary<string, TokenBucket>();


        public MyTokenBucketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;

            if (!string.IsNullOrEmpty(path))
            {

                TokenBucket token = pathTokenBucket.GetOrAdd(path, new TokenBucket { Capacity = 10, LastRefill = DateTime.UtcNow, Tokens = 10, RefillRate = 1 });

                lock (token)
                {
                    var now = DateTime.UtcNow;
                    var seconds = (now - token.LastRefill).TotalSeconds;
                    token.Tokens = Math.Min(token.Capacity, token.Tokens + seconds * token.RefillRate);
                    token.LastRefill = now;


                    if (token.Tokens >= 1)
                    {
                        token.Tokens--;
                    }
                    else
                    {
                        context.Response.StatusCode = 429;
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
