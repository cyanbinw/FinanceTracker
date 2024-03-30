namespace FinanceTracker.Service.Models.BillModels
{
    public class BillModel
    {
        public string BillNumber { get; set; }
        public string BillName { get; set; }
        public string Type { get; set; }
        public decimal Account { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }

        public BillModel()
        {
            this.BillNumber = string.Empty;
            this.BillName = string.Empty;
            this.Type = string.Empty;
            this.Account = 0;
            this.Date = DateTime.Now;
            this.Remarks = string.Empty;
        }
    }
}
