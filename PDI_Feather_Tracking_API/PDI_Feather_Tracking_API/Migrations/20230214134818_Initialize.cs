using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PDI_Feather_Tracking_API.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ModuleAccess",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleAccess", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SkuType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(1)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkuType", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TareWeightSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChildCount = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareWeightSetting", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ModuleAccessid = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLevels_ModuleAccess_ModuleAccessid",
                        column: x => x.ModuleAccessid,
                        principalTable: "ModuleAccess",
                        principalColumn: "id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InventoryRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SkuTypeId = table.Column<int>(type: "int", nullable: false),
                    BatchNo = table.Column<string>(type: "longtext", nullable: false),
                    GrossWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TareWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NettWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncomingDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OutgoingDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OutgoingPic = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryRecords_SkuType_SkuTypeId",
                        column: x => x.SkuTypeId,
                        principalTable: "SkuType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    EmployeeNo = table.Column<string>(type: "longtext", nullable: false),
                    UserLevelId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsSignedIn = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserLevels_UserLevelId",
                        column: x => x.UserLevelId,
                        principalTable: "UserLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "TareWeightSetting",
                columns: new[] { "Id", "ChildCount", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy", "Weight" },
                values: new object[] { 1, 0, new DateTime(2023, 2, 14, 21, 48, 18, 131, DateTimeKind.Local).AddTicks(5501), 1, new DateTime(2023, 2, 14, 21, 48, 18, 131, DateTimeKind.Local).AddTicks(5502), 1, 0m });

            migrationBuilder.InsertData(
                table: "UserLevels",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "ModuleAccessid", "Name", "Status", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 2, 14, 21, 48, 18, 91, DateTimeKind.Local).AddTicks(5641), 1, null, "SysAdmin", true, new DateTime(2023, 2, 14, 21, 48, 18, 91, DateTimeKind.Local).AddTicks(5648), 1 },
                    { 2, new DateTime(2023, 2, 14, 21, 48, 18, 91, DateTimeKind.Local).AddTicks(5650), 1, null, "Admin", true, new DateTime(2023, 2, 14, 21, 48, 18, 91, DateTimeKind.Local).AddTicks(5651), 1 },
                    { 3, new DateTime(2023, 2, 14, 21, 48, 18, 91, DateTimeKind.Local).AddTicks(5652), 1, null, "Supervisor", true, new DateTime(2023, 2, 14, 21, 48, 18, 91, DateTimeKind.Local).AddTicks(5652), 1 },
                    { 4, new DateTime(2023, 2, 14, 21, 48, 18, 91, DateTimeKind.Local).AddTicks(5654), 1, null, "Operator", true, new DateTime(2023, 2, 14, 21, 48, 18, 91, DateTimeKind.Local).AddTicks(5654), 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "EmployeeNo", "IsSignedIn", "Password", "Status", "UpdatedAt", "UpdatedBy", "UserLevelId", "Username" },
                values: new object[] { 1, new DateTime(2023, 2, 14, 21, 48, 18, 131, DateTimeKind.Local).AddTicks(5239), 1, "SA001", false, "r77qptZQaXl51i54y9wiaU98SMQTu65/pfjH/izK4al+13dg", true, new DateTime(2023, 2, 14, 21, 48, 18, 131, DateTimeKind.Local).AddTicks(5251), 1, 1, "sysadmin" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryRecords_SkuTypeId",
                table: "InventoryRecords",
                column: "SkuTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLevels_ModuleAccessid",
                table: "UserLevels",
                column: "ModuleAccessid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserLevelId",
                table: "Users",
                column: "UserLevelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryRecords");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "TareWeightSetting");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SkuType");

            migrationBuilder.DropTable(
                name: "UserLevels");

            migrationBuilder.DropTable(
                name: "ModuleAccess");
        }
    }
}
