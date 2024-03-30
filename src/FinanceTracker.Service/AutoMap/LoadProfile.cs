using AutoMapper;
using FinanceTracker.EntityFramework.Data;
using FinanceTracker.Service.Models.BillModels;

namespace FinanceTracker.Service.AutoMap
{
    public class LoadProfile : Profile
    {
        public LoadProfile() 
        {
            CreateMap<BillModel, Bill>();
            CreateMap<Bill, BillDetailModel>();
        }
    }
}
