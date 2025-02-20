using FinanceTracker.EntityFramework.Models;

namespace FinanceTracker.EntityFramework.Entity
{
    public interface IBaseWorker<T> where T : IBaseFields, new()
    {
        public Task AddAsync(T data);

        public Task UpdateAsync(T data);

        public Task<T> GetByIdAsync(int id);

        public Task<List<T>> GetAllAsync();

        public Task RemoveAsync(T id);
    }
}
