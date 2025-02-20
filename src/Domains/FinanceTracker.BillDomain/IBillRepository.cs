using FinanceTracker.BillDomain.Models.BillModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.BillDomain
{
    public interface IBillRepository
    {
        public Task<BillDetailModel> CreateBillAsync(BillModel data);

        public Task<bool> DeleteBillAsync(int id);

        public Task<bool> UpdateBillAsync(BillDetailModel data);

        public Task<List<BillDetailModel>> GetBillAsync();
        public Task<BillDetailModel> GetBillByIdAsync(int id);
    }
}
