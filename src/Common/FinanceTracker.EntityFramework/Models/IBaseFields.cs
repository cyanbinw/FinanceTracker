namespace FinanceTracker.EntityFramework.Models
{
    public interface IBaseFields
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
