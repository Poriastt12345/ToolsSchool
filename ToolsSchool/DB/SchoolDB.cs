using Microsoft.EntityFrameworkCore;
using System;
using ToolsSchool.Entities;

namespace ToolsSchool.DB
{
    public class SchoolDB:DbContext
    {
        public SchoolDB(DbContextOptions<SchoolDB> options) : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<QualityLevel> Levels { get; set; }
        public DbSet<Tools> Tools { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تنظیمات پیش‌فرض برای Ad
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(a => a.Category);
                entity.HasIndex(a => a.Status);
                entity.HasIndex(a => a.CreatedDate);
                entity.HasIndex(a => a.City);

                entity.Property(a => a.ProductName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(a => a.Category)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(a => a.Price)
                    .HasColumnType("decimal(18,2)");

                entity.Property(a => a.UsageCondition)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValue("New");

                entity.Property(a => a.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(a => a.Description)
                    .HasMaxLength(1000);

                entity.Property(a => a.ImagePath)
                    .HasMaxLength(500);

                entity.Property(a => a.Status)
                    .HasMaxLength(50)
                    .HasDefaultValue("Pending");

                entity.Property(a => a.CreatedDate)
                    .HasDefaultValueSql("GETDATE()"); // برای SQL Server
                                                      // .HasDefaultValueSql("CURRENT_TIMESTAMP"); // برای MySQL

                // تنظیم تاریخ انقضا 30 روز پس از ایجاد
                entity.Property(a => a.ExpiryDate)
                    .HasComputedColumnSql("DATEADD(day, 30, CreatedDate)"); // SQL Server
                                                                            // .HasComputedColumnSql("DATE_ADD(CreatedDate, INTERVAL 30 DAY)"); // MySQL
            });
        }
    }
}
