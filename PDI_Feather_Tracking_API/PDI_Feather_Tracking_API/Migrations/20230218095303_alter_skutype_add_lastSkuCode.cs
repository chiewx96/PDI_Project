using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDIFeatherTrackingAPI.Migrations
{
    /// <inheritdoc />
    public partial class alterskutypeaddlastSkuCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastSkuCode",
                table: "SkuType",
                type: "longtext",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TareWeightSetting",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 17, 53, 3, 673, DateTimeKind.Local).AddTicks(5380), new DateTime(2023, 2, 18, 17, 53, 3, 673, DateTimeKind.Local).AddTicks(5381) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 17, 53, 3, 672, DateTimeKind.Local).AddTicks(1553), new DateTime(2023, 2, 18, 17, 53, 3, 672, DateTimeKind.Local).AddTicks(1561) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 17, 53, 3, 672, DateTimeKind.Local).AddTicks(1563), new DateTime(2023, 2, 18, 17, 53, 3, 672, DateTimeKind.Local).AddTicks(1564) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 17, 53, 3, 672, DateTimeKind.Local).AddTicks(1565), new DateTime(2023, 2, 18, 17, 53, 3, 672, DateTimeKind.Local).AddTicks(1566) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 17, 53, 3, 672, DateTimeKind.Local).AddTicks(1567), new DateTime(2023, 2, 18, 17, 53, 3, 672, DateTimeKind.Local).AddTicks(1568) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 17, 53, 3, 673, DateTimeKind.Local).AddTicks(5330), new DateTime(2023, 2, 18, 17, 53, 3, 673, DateTimeKind.Local).AddTicks(5333) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSkuCode",
                table: "SkuType");

            migrationBuilder.UpdateData(
                table: "TareWeightSetting",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 17, 17, 30, 831, DateTimeKind.Local).AddTicks(4011), new DateTime(2023, 2, 18, 17, 17, 30, 831, DateTimeKind.Local).AddTicks(4012) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 17, 17, 30, 829, DateTimeKind.Local).AddTicks(9628), new DateTime(2023, 2, 18, 17, 17, 30, 829, DateTimeKind.Local).AddTicks(9637) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 17, 17, 30, 829, DateTimeKind.Local).AddTicks(9639), new DateTime(2023, 2, 18, 17, 17, 30, 829, DateTimeKind.Local).AddTicks(9639) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 17, 17, 30, 829, DateTimeKind.Local).AddTicks(9640), new DateTime(2023, 2, 18, 17, 17, 30, 829, DateTimeKind.Local).AddTicks(9641) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 17, 17, 30, 829, DateTimeKind.Local).AddTicks(9642), new DateTime(2023, 2, 18, 17, 17, 30, 829, DateTimeKind.Local).AddTicks(9643) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 17, 17, 30, 831, DateTimeKind.Local).AddTicks(3960), new DateTime(2023, 2, 18, 17, 17, 30, 831, DateTimeKind.Local).AddTicks(3964) });
        }
    }
}
