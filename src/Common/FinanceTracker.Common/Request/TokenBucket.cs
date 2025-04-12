using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Common.Request
{
    public class TokenBucket
    {
        public int Capacity { get; set; } = 10;
        public int RefillRate { get; set; } = 1;
        public double Tokens { get; set; } = 10;
        public DateTime LastRefill { get; set; } = DateTime.UtcNow;
    }
}
