using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PDI_Feather_Tracking_API.Models.RequestModel;

namespace PDI_Feather_Tracking_API.Models;

public partial class PDIFeatherTrackingDbContext : DbContext
{
    public PDIFeatherTrackingDbContext(DbContextOptions<PDIFeatherTrackingDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<InventoryRecords> InventoryRecords { get; set; }

    public virtual DbSet<Module> Module { get; set; }

    public virtual DbSet<SkuType> SkuType { get; set; }

    public virtual DbSet<TareWeightSetting> TareWeightSetting { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLevel> UserLevels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(t => new { t.Id });
        modelBuilder.Entity<UserLevel>().HasKey(t => new { t.Id });
        modelBuilder.Entity<InventoryRecords>().HasKey(t => new { t.Id });
        modelBuilder.Entity<Module>().HasKey(t => new { t.Id });
        modelBuilder.Entity<SkuType>().HasKey(t => new { t.Id });
        modelBuilder.Entity<TareWeightSetting>().HasKey(t => new { t.Id });

        // Seeder

        modelBuilder.Entity<Module>().HasData(
            new Module { Id = 1, Name = "user-level" },
            new Module { Id = 2, Name = "user" },
            new Module { Id = 3, Name = "sku-type" },
            new Module { Id = 4, Name = "tare-weight-setting" },
            new Module { Id = 5, Name = "incoming" },
            new Module { Id = 6, Name = "outgoing" },
            new Module { Id = 7, Name = "reporting" }
           );


        modelBuilder.Entity<UserLevel>().HasData(
            new UserLevel
            {
                Id = 1,
                Name = "SysAdmin",
                Status = true,
                CreatedBy = 1,
                UpdatedBy = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ModuleAccess = "[{\"Module\":{\"Id\":1,\"Name\":\"user-level\"},\"Status\":1},{\"Module\":{\"Id\":2,\"Name\":\"user\"},\"Status\":1}," +
                "{\"Module\":{\"Id\":3,\"Name\":\"sku-type\"},\"Status\":1},{\"Module\":{\"Id\":4,\"Name\":\"tare-weight-setting\"},\"Status\":1}," +
                "{\"Module\":{\"Id\":5,\"Name\":\"incoming\"},\"Status\":1},{\"Module\":{\"Id\":6,\"Name\":\"outgoing\"},\"Status\":1}," +
                "{\"Module\":{\"Id\":7,\"Name\":\"reporting\"},\"Status\":1}]"
            },
            new UserLevel { Id = 2, Name = "Admin", Status = true, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new UserLevel { Id = 3, Name = "Supervisor", Status = true, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new UserLevel { Id = 4, Name = "Operator", Status = true, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "sysadmin", Password = General.Encrypt("abc123"), EmployeeNo = "SA001", Status = true, UserLevelId = 1, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );

        modelBuilder.Entity<TareWeightSetting>().HasData(
           new TareWeightSetting { Id = 1, Weight = 0, ChildCount = 0, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
           );

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
