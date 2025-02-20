using AutoMapper;
using FinanceTracker.BillDomain.Models.BillModels;
using FinanceTracker.EntityFramework;
using FinanceTracker.EntityFramework.Data;
using FinanceTracker.EntityFramework.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core.Tokens;

namespace FinanceTracker.BillDomain
{
    public class BillRepository : IBillRepository
    {
        readonly ILogger<BillRepository> logger;
        readonly PostgreSqlContext context;
        readonly IBillWorker billWorker;
        readonly IMapper mapper;

        public BillRepository(ILogger<BillRepository> logger ,IBillWorker billWorker, IMapper mapper, PostgreSqlContext context) 
        {
            this.logger = logger;
            this.billWorker = billWorker;
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<BillDetailModel> CreateBillAsync(BillModel data)
        {
            var value = mapper.Map<Bill>(data);

            await billWorker.AddAsync(value);

            return mapper.Map<BillDetailModel>(value);
        }

        public Task<bool> DeleteBillAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateBillAsync(BillDetailModel data)
        {
            
            var value = await billWorker.GetByIdAsync(data.Id);
            mapper.Map(data, value);
            try
            {
                await billWorker.UpdateAsync(value);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Task<List<BillDetailModel>> GetBillAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BillDetailModel> GetBillByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
