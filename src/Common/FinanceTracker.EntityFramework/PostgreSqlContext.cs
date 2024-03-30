using FinanceTracker.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.EntityFramework
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
            OnBillCreaating(modelBuilder);
        }

        private void OnBillCreaating(ModelBuilder modelBuilder)
        {
            // 配置Bill实体的主键  
            modelBuilder.Entity<Bill>()
                .ToTable("Bill")
                .Property(e => e.Id)
                .ValueGeneratedOnAdd(); // 表示在添加新实体时生成值  

            modelBuilder.Entity<Bill>()
                .Property(e => e.CreateDate)
                .HasColumnType("timestamp with time zone")
                .HasConversion(
                    v => v.ToUniversalTime(), // 写入数据库时转换为UTC  
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // 从数据库读取时指定为UTC Kind  
                );
            modelBuilder.Entity<Bill>()
                .Property(e => e.UpdateDate)
                .HasColumnType("timestamp with time zone")
                .HasConversion(
                    v => v.ToUniversalTime(), // 写入数据库时转换为UTC  
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // 从数据库读取时指定为UTC Kind  
                 );
            modelBuilder.Entity<Bill>()
                    .Property(e => e.Date)
                    .HasColumnType("timestamp with time zone")
                    .HasConversion(
                        v => v.ToUniversalTime(), // 写入数据库时转换为UTC  
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // 从数据库读取时指定为UTC Kind  
                     );
        }
    }
}
