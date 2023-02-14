using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using EFWeightScan;

namespace WeightScanAPI.Models;

public partial class PDIFeatherTrackingDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public PDIFeatherTrackingDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public PDIFeatherTrackingDbContext(DbContextOptions<PDIFeatherTrackingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InventoryRecords> InventoryRecords { get; set; }

    public virtual DbSet<Module> Module { get; set; }

    public virtual DbSet<SkuType> SkuType { get; set; }

    public virtual DbSet<TareWeightSetting> TareWeightSetting { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLevel> UserLevels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMySQL(_configuration.GetValue<string>("ConnectionStrings:PDIFeatherTracking"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(t => new { t.Id });
        modelBuilder.Entity<UserLevel>().HasKey(t => new { t.Id });
        modelBuilder.Entity<InventoryRecords>().HasKey(t => new { t.Id });
        modelBuilder.Entity<Module>().HasKey(t => new { t.Id });
        modelBuilder.Entity<SkuType>().HasKey(t => new { t.Id });
        modelBuilder.Entity<TareWeightSetting>().HasKey(t => new { t.Id });

        // Seeder
        modelBuilder.Entity<UserLevel>().HasData(
            new UserLevel { Id = 1, Name = "SysAdmin", Status = true, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new UserLevel { Id = 2, Name = "Admin", Status = true, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new UserLevel { Id = 3, Name = "Supervisor", Status = true, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new UserLevel { Id = 4, Name = "Operator", Status = true, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "sysadmin", Password = General.HashPassword("abc123"), EmployeeNo = "SA001", Status = true, UserLevelId = 1, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );

        modelBuilder.Entity<TareWeightSetting>().HasData(
           new TareWeightSetting { Id = 1, Weight = 0, ChildCount = 0, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
           );
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
