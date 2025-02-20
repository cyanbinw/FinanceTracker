using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTracker.EntityFramework.Models
{
    public class BaseFields : IBaseFields
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public BaseStatusType Status {  get; set; } = BaseStatusType.Inserted;

        public BaseFields() 
        {
        }
    }

    public enum BaseStatusType
    {
        Inserted = 0,
        Updated = 1,
        PendingReview = 2,
        Deleted = 99
    }
}
