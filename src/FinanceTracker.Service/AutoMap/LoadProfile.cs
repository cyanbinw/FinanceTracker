using AutoMapper;
using FinanceTracker.EntityFramework.Data;
using FinanceTracker.BillDomain.Models.BillModels;

namespace FinanceTracker.Service.AutoMap
{
    public class LoadProfile : Profile
    {
        public LoadProfile() 
        {
            CreateMap<BillModel, Bill>();
            CreateMap<Bill, BillDetailModel>();
            CreateMap<BillDetailModel, Bill>();
        }
    }
}
