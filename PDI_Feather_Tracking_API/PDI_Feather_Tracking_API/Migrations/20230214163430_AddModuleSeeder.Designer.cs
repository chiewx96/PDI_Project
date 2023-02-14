﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeightScanAPI.Models;

#nullable disable

namespace WeightScanAPI.Migrations
{
    [DbContext(typeof(PDIFeatherTrackingDbContext))]
    [Migration("20230214163430_AddModuleSeeder")]
    partial class AddModuleSeeder
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WeightScanAPI.DataModel.ModuleAccess", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("ModuleAccess");
                });

            modelBuilder.Entity("WeightScanAPI.Models.InventoryRecords", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BatchNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<decimal>("GrossWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("IncomingDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("NettWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("OutgoingDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("OutgoingPic")
                        .HasColumnType("int");

                    b.Property<int>("SkuTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("TareWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SkuTypeId");

                    b.ToTable("InventoryRecords");
                });

            modelBuilder.Entity("WeightScanAPI.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Module");
                });

            modelBuilder.Entity("WeightScanAPI.Models.SkuType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SkuType");
                });

            modelBuilder.Entity("WeightScanAPI.Models.TareWeightSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ChildCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("TareWeightSetting");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChildCount = 0,
                            CreatedAt = new DateTime(2023, 2, 15, 0, 34, 30, 529, DateTimeKind.Local).AddTicks(3084),
                            CreatedBy = 1,
                            UpdatedAt = new DateTime(2023, 2, 15, 0, 34, 30, 529, DateTimeKind.Local).AddTicks(3084),
                            UpdatedBy = 1,
                            Weight = 0m
                        });
                });

            modelBuilder.Entity("WeightScanAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsSignedIn")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<int>("UserLevelId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("UserLevelId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 2, 15, 0, 34, 30, 529, DateTimeKind.Local).AddTicks(2796),
                            CreatedBy = 1,
                            EmployeeNo = "SA001",
                            IsSignedIn = false,
                            Password = "Q/iS86IdHUgC3V8frPyd61GB+jqnhwtyE8AI9ijFed37IFyX",
                            Status = true,
                            UpdatedAt = new DateTime(2023, 2, 15, 0, 34, 30, 529, DateTimeKind.Local).AddTicks(2810),
                            UpdatedBy = 1,
                            UserLevelId = 1,
                            Username = "sysadmin"
                        });
                });

            modelBuilder.Entity("WeightScanAPI.Models.UserLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<int?>("ModuleAccessid")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModuleAccessid");

                    b.ToTable("UserLevels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7925),
                            CreatedBy = 1,
                            Name = "SysAdmin",
                            Status = true,
                            UpdatedAt = new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7932),
                            UpdatedBy = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7934),
                            CreatedBy = 1,
                            Name = "Admin",
                            Status = true,
                            UpdatedAt = new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7935),
                            UpdatedBy = 1
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7936),
                            CreatedBy = 1,
                            Name = "Supervisor",
                            Status = true,
                            UpdatedAt = new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7937),
                            UpdatedBy = 1
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7938),
                            CreatedBy = 1,
                            Name = "Operator",
                            Status = true,
                            UpdatedAt = new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7939),
                            UpdatedBy = 1
                        });
                });

            modelBuilder.Entity("WeightScanAPI.Models.InventoryRecords", b =>
                {
                    b.HasOne("WeightScanAPI.Models.SkuType", "SkuType")
                        .WithMany("InventoryRecords")
                        .HasForeignKey("SkuTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SkuType");
                });

            modelBuilder.Entity("WeightScanAPI.Models.User", b =>
                {
                    b.HasOne("WeightScanAPI.Models.UserLevel", "UserLevel")
                        .WithMany("Users")
                        .HasForeignKey("UserLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserLevel");
                });

            modelBuilder.Entity("WeightScanAPI.Models.UserLevel", b =>
                {
                    b.HasOne("WeightScanAPI.DataModel.ModuleAccess", "ModuleAccess")
                        .WithMany()
                        .HasForeignKey("ModuleAccessid");

                    b.Navigation("ModuleAccess");
                });

            modelBuilder.Entity("WeightScanAPI.Models.SkuType", b =>
                {
                    b.Navigation("InventoryRecords");
                });

            modelBuilder.Entity("WeightScanAPI.Models.UserLevel", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
