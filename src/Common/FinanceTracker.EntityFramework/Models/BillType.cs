using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.EntityFramework.Models
{
    public class BillType : BaseFields
    {
        public string TypeName { get; set; }
        public string Description { get; set; }

        public BillType() 
        {
            this.TypeName = string.Empty;
            this.Description = string.Empty;
        }
    }
}
