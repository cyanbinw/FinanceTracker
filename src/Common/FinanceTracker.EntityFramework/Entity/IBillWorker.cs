using FinanceTracker.EntityFramework.Models;

namespace FinanceTracker.EntityFramework.Entity
{
    public interface IBillWorker<T> : IBaseWorker<T> where T : IBaseFields, new()
    {
    }
}
