namespace FinanceTracker.BillDomain.Models.BillModels
{
    public class BillDetailModel : BillModel
    {
        public int Id { get; set; }

        public BillDetailModel() { }

        public BillDetailModel(int id,BillModel data) 
        {
            this.BillNumber = data.BillNumber;
            this.BillName = data.BillName;
            this.Remarks = data.Remarks;
            this.Account = data.Account;
            this.Date = data.Date;
            this.Id = id;
        }
    }
}
