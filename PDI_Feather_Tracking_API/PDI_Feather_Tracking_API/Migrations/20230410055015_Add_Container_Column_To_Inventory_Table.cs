using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDIFeatherTrackingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddContainerColumnToInventoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OutgoingContainer",
                table: "InventoryRecords",
                type: "longtext",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TareWeightSetting",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 10, 13, 50, 15, 764, DateTimeKind.Local).AddTicks(688), new DateTime(2023, 4, 10, 13, 50, 15, 764, DateTimeKind.Local).AddTicks(689) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 10, 13, 50, 15, 762, DateTimeKind.Local).AddTicks(9329), new DateTime(2023, 4, 10, 13, 50, 15, 762, DateTimeKind.Local).AddTicks(9335) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 10, 13, 50, 15, 762, DateTimeKind.Local).AddTicks(9337), new DateTime(2023, 4, 10, 13, 50, 15, 762, DateTimeKind.Local).AddTicks(9337) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 10, 13, 50, 15, 762, DateTimeKind.Local).AddTicks(9339), new DateTime(2023, 4, 10, 13, 50, 15, 762, DateTimeKind.Local).AddTicks(9339) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 10, 13, 50, 15, 762, DateTimeKind.Local).AddTicks(9340), new DateTime(2023, 4, 10, 13, 50, 15, 762, DateTimeKind.Local).AddTicks(9341) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 10, 13, 50, 15, 764, DateTimeKind.Local).AddTicks(603), new DateTime(2023, 4, 10, 13, 50, 15, 764, DateTimeKind.Local).AddTicks(604) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OutgoingContainer",
                table: "InventoryRecords");

            migrationBuilder.UpdateData(
                table: "TareWeightSetting",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 3, 28, 22, 5, 13, 928, DateTimeKind.Local).AddTicks(771), new DateTime(2023, 3, 28, 22, 5, 13, 928, DateTimeKind.Local).AddTicks(772) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 3, 28, 22, 5, 13, 926, DateTimeKind.Local).AddTicks(7552), new DateTime(2023, 3, 28, 22, 5, 13, 926, DateTimeKind.Local).AddTicks(7559) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 3, 28, 22, 5, 13, 926, DateTimeKind.Local).AddTicks(7561), new DateTime(2023, 3, 28, 22, 5, 13, 926, DateTimeKind.Local).AddTicks(7561) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 3, 28, 22, 5, 13, 926, DateTimeKind.Local).AddTicks(7563), new DateTime(2023, 3, 28, 22, 5, 13, 926, DateTimeKind.Local).AddTicks(7563) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 3, 28, 22, 5, 13, 926, DateTimeKind.Local).AddTicks(7564), new DateTime(2023, 3, 28, 22, 5, 13, 926, DateTimeKind.Local).AddTicks(7565) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 3, 28, 22, 5, 13, 928, DateTimeKind.Local).AddTicks(688), new DateTime(2023, 3, 28, 22, 5, 13, 928, DateTimeKind.Local).AddTicks(690) });
        }
    }
}
