namespace FinanceTracker.EntityFramework.Entity
{
    public interface IBaseWorker<T>
    {
        public Task AddAsync(T data);

        public Task UpdateAsync(T data);

        public Task<T> GetByIdAsync(int id);

        public Task<List<T>> GetAllAsync();

        public Task RemoveAsync(T id);
    }
}
