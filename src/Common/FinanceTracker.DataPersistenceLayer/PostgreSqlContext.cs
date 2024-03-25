using FinanceTracker.DataPersistenceLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.DataPersistenceLayer
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Bill> Bills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置Bill实体的主键  
            modelBuilder.Entity<Bill>()
                .ToTable("Bill")
                .HasKey(b => b.Id);
                

            // 其他配置...  
        }
    }
}
