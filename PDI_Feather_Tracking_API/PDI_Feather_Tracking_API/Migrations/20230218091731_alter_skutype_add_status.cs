using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDIFeatherTrackingAPI.Migrations
{
    /// <inheritdoc />
    public partial class alterskutypeaddstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "SkuType",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SkuType");

            migrationBuilder.UpdateData(
                table: "TareWeightSetting",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 2, 34, 36, 657, DateTimeKind.Local).AddTicks(308), new DateTime(2023, 2, 18, 2, 34, 36, 657, DateTimeKind.Local).AddTicks(309) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 2, 34, 36, 655, DateTimeKind.Local).AddTicks(5370), new DateTime(2023, 2, 18, 2, 34, 36, 655, DateTimeKind.Local).AddTicks(5378) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 2, 34, 36, 655, DateTimeKind.Local).AddTicks(5380), new DateTime(2023, 2, 18, 2, 34, 36, 655, DateTimeKind.Local).AddTicks(5380) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 2, 34, 36, 655, DateTimeKind.Local).AddTicks(5382), new DateTime(2023, 2, 18, 2, 34, 36, 655, DateTimeKind.Local).AddTicks(5382) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 2, 34, 36, 655, DateTimeKind.Local).AddTicks(5384), new DateTime(2023, 2, 18, 2, 34, 36, 655, DateTimeKind.Local).AddTicks(5384) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 2, 34, 36, 657, DateTimeKind.Local).AddTicks(182), new DateTime(2023, 2, 18, 2, 34, 36, 657, DateTimeKind.Local).AddTicks(188) });
        }
    }
}
