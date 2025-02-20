using FinanceTracker.EntityFramework.Data;
using FinanceTracker.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Web.LayoutRenderers;
using System.Runtime.InteropServices;

namespace FinanceTracker.EntityFramework.Entity
{
    public class BaseDBWorker<T, R> : AbstractDBWorker<T>  where T : class, IBaseFields, new() where R : class
    {
        protected readonly PostgreSqlContext context;
        protected readonly ILogger<R> logger;

        public BaseDBWorker(ILogger<R> logger, PostgreSqlContext context) 
        {
            this.logger = logger;
            this.context = context;
        }

        public override async Task AddAsync(T data)
        {
            try
            {
                await context.AddAsync(data);
                var result = await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // 处理数据库更新异常  
                logger.LogError(ex, "DB update faile: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // 处理无效操作异常  
                logger.LogError(ex, "DB invalid operation: " + ex.Message);
            }
            catch (Exception ex)
            {
                // 捕获其他所有异常  
                logger.LogError(ex, "DB error: " + ex.Message);
            }          
        }

        public override async Task UpdateAsync(T data)
        {
            try
            {
                data.Status = BaseStatusType.Updated;
                data.UpdateDate = DateTime.Now;
                context.Update(data);
                var result = await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // 处理数据库更新异常  
                logger.LogError(ex, "DB update faile: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // 处理无效操作异常  
                logger.LogError(ex, "IDB invalid operation: " + ex.Message);
            }
            catch (Exception ex)
            {
                // 捕获其他所有异常  
                logger.LogError(ex, "DB error: " + ex.Message);
            }
        }

        public override async Task<T> GetByIdAsync(int id)
        {
            T result = new();

            try
            {
                result = await context.FindAsync<T>(id) ?? new T();
            }
            catch (DbUpdateException ex)
            {
                // 处理数据库更新异常  
                logger.LogError(ex, "DB update faile: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // 处理无效操作异常  
                logger.LogError(ex, "DB invalid operation: " + ex.Message);
            }
            catch (Exception ex)
            {
                // 捕获其他所有异常  
                logger.LogError(ex, "DB error: " + ex.Message);
            }

            return result;
        }

        // must override
        public override async Task<List<T>> GetAllAsync()
        {
            await Task.Delay(1000);
            return new List<T>();
        }

        public override async Task RemoveAsync(T data)
        {
            try
            {
                context.Remove(data);
                var result = await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // 处理数据库更新异常  
                logger.LogError(ex, "DB update faile: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // 处理无效操作异常  
                logger.LogError(ex, "DB invalid operation: " + ex.Message);
            }
            catch (Exception ex)
            {
                // 捕获其他所有异常  
                logger.LogError(ex, "DB error: " + ex.Message);
            }
        }
    }
}
