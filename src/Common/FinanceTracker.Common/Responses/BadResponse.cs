using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Common.Responses
{
    public class BadResponse
    {
        public string Message { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }

        public BadResponse() 
        {
            this.Errors = new Dictionary<string, string[]>();
            this.Message = string.Empty;
        }

        public BadResponse(string message, IDictionary<string, string[]> errors)
        {
            Message = message;
            Errors = errors;
        }
    }
}
