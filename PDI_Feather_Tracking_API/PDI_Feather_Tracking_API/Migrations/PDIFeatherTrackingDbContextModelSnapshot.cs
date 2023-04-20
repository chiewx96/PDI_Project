﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PDI_Feather_Tracking_API.Models;

#nullable disable

namespace PDIFeatherTrackingAPI.Migrations
{
    [DbContext(typeof(PDIFeatherTrackingDbContext))]
    partial class PDIFeatherTrackingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PDI_Feather_Tracking_API.Models.InventoryRecords", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BatchNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CancelStatus")
                        .HasColumnType("int")
                        .HasComment("Cancel : 1, Not cancel : 0");

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

                    b.Property<string>("OutgoingContainer")
                        .HasColumnType("longtext");

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

            modelBuilder.Entity("PDI_Feather_Tracking_API.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Module");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "user-level"
                        },
                        new
                        {
                            Id = 2,
                            Name = "user"
                        },
                        new
                        {
                            Id = 3,
                            Name = "sku-type"
                        },
                        new
                        {
                            Id = 4,
                            Name = "tare-weight-setting"
                        },
                        new
                        {
                            Id = 5,
                            Name = "incoming"
                        },
                        new
                        {
                            Id = 6,
                            Name = "outgoing"
                        },
                        new
                        {
                            Id = 7,
                            Name = "reporting"
                        });
                });

            modelBuilder.Entity("PDI_Feather_Tracking_API.Models.SkuType", b =>
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

                    b.Property<string>("LastSkuCode")
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SkuType");
                });

            modelBuilder.Entity("PDI_Feather_Tracking_API.Models.TareWeightSetting", b =>
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
                            CreatedAt = new DateTime(2023, 4, 20, 14, 18, 56, 491, DateTimeKind.Local).AddTicks(7964),
                            CreatedBy = 1,
                            UpdatedAt = new DateTime(2023, 4, 20, 14, 18, 56, 491, DateTimeKind.Local).AddTicks(7964),
                            UpdatedBy = 1,
                            Weight = 0m
                        });
                });

            modelBuilder.Entity("PDI_Feather_Tracking_API.Models.User", b =>
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
                            CreatedAt = new DateTime(2023, 4, 20, 14, 18, 56, 491, DateTimeKind.Local).AddTicks(7842),
                            CreatedBy = 1,
                            EmployeeNo = "SA001",
                            Password = "2yw689CCSPkvtkj6VNBpug==",
                            Status = true,
                            UpdatedAt = new DateTime(2023, 4, 20, 14, 18, 56, 491, DateTimeKind.Local).AddTicks(7844),
                            UpdatedBy = 1,
                            UserLevelId = 1,
                            Username = "sysadmin"
                        });
                });

            modelBuilder.Entity("PDI_Feather_Tracking_API.Models.UserLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("ModuleAccess")
                        .HasColumnType("longtext");

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

                    b.ToTable("UserLevels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4521),
                            CreatedBy = 1,
                            ModuleAccess = "[{\"Module\":{\"Id\":1,\"Name\":\"user-level\"},\"Status\":1},{\"Module\":{\"Id\":2,\"Name\":\"user\"},\"Status\":1},{\"Module\":{\"Id\":3,\"Name\":\"sku-type\"},\"Status\":1},{\"Module\":{\"Id\":4,\"Name\":\"tare-weight-setting\"},\"Status\":1},{\"Module\":{\"Id\":5,\"Name\":\"incoming\"},\"Status\":1},{\"Module\":{\"Id\":6,\"Name\":\"outgoing\"},\"Status\":1},{\"Module\":{\"Id\":7,\"Name\":\"reporting\"},\"Status\":1}]",
                            Name = "SysAdmin",
                            Status = true,
                            UpdatedAt = new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4529),
                            UpdatedBy = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4531),
                            CreatedBy = 1,
                            Name = "Admin",
                            Status = true,
                            UpdatedAt = new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4532),
                            UpdatedBy = 1
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4533),
                            CreatedBy = 1,
                            Name = "Supervisor",
                            Status = true,
                            UpdatedAt = new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4534),
                            UpdatedBy = 1
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4535),
                            CreatedBy = 1,
                            Name = "Operator",
                            Status = true,
                            UpdatedAt = new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4536),
                            UpdatedBy = 1
                        });
                });

            modelBuilder.Entity("PDI_Feather_Tracking_API.Models.InventoryRecords", b =>
                {
                    b.HasOne("PDI_Feather_Tracking_API.Models.SkuType", "SkuType")
                        .WithMany("InventoryRecords")
                        .HasForeignKey("SkuTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SkuType");
                });

            modelBuilder.Entity("PDI_Feather_Tracking_API.Models.User", b =>
                {
                    b.HasOne("PDI_Feather_Tracking_API.Models.UserLevel", "UserLevel")
                        .WithMany("Users")
                        .HasForeignKey("UserLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserLevel");
                });

            modelBuilder.Entity("PDI_Feather_Tracking_API.Models.SkuType", b =>
                {
                    b.Navigation("InventoryRecords");
                });

            modelBuilder.Entity("PDI_Feather_Tracking_API.Models.UserLevel", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
