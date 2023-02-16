using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_WPF.Global
{
    public class FeatherDbContext : DbContext
    {
        #region Contructor
        public FeatherDbContext(DbContextOptions<FeatherDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        #endregion

        #region Public properties
        public virtual DbSet<InventoryRecords> InventoryRecords { get; set; }

        public virtual DbSet<Module> Module { get; set; }

        public virtual DbSet<SkuType> SkuType { get; set; }

        public virtual DbSet<TareWeightSetting> TareWeightSetting { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserLevel> UserLevels { get; set; }
        #endregion

        #region Overridden method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(t => new { t.Id });
            modelBuilder.Entity<UserLevel>().HasKey(t => new { t.Id });
            modelBuilder.Entity<InventoryRecords>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Module>().HasKey(t => new { t.Id });
            modelBuilder.Entity<SkuType>().HasKey(t => new { t.Id });
            modelBuilder.Entity<TareWeightSetting>().HasKey(t => new { t.Id });
            base.OnModelCreating(modelBuilder);
        }
        #endregion


        #region Private method
        private List<UserLevel> GetUserLevels()
        {
            return new List<UserLevel>
        {
            new UserLevel { Id = 1, Name = "SysAdmin", Status = true, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new UserLevel { Id = 2, Name = "Admin", Status = true, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new UserLevel { Id = 3, Name = "Supervisor", Status = true, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new UserLevel { Id = 4, Name = "Operator", Status = true, CreatedBy = 1, UpdatedBy = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
        };
        }
        #endregion
    }
}
