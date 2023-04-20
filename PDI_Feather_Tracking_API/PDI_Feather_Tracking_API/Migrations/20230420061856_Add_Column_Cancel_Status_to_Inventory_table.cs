using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDIFeatherTrackingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnCancelStatustoInventorytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CancelStatus",
                table: "InventoryRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Cancel : 1, Not cancel : 0");

            migrationBuilder.UpdateData(
                table: "TareWeightSetting",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 20, 14, 18, 56, 491, DateTimeKind.Local).AddTicks(7964), new DateTime(2023, 4, 20, 14, 18, 56, 491, DateTimeKind.Local).AddTicks(7964) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4521), new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4529) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4531), new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4532) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4533), new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4534) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4535), new DateTime(2023, 4, 20, 14, 18, 56, 490, DateTimeKind.Local).AddTicks(4536) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 20, 14, 18, 56, 491, DateTimeKind.Local).AddTicks(7842), new DateTime(2023, 4, 20, 14, 18, 56, 491, DateTimeKind.Local).AddTicks(7844) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelStatus",
                table: "InventoryRecords");

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
    }
}
