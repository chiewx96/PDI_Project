using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PDIFeatherTrackingAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemakeSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "IsSignedIn",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "reporting");

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
                columns: new[] { "CreatedAt", "ModuleAccess", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 3, 28, 22, 5, 13, 926, DateTimeKind.Local).AddTicks(7552), "[{\"Module\":{\"Id\":1,\"Name\":\"user-level\"},\"Status\":1},{\"Module\":{\"Id\":2,\"Name\":\"user\"},\"Status\":1},{\"Module\":{\"Id\":3,\"Name\":\"sku-type\"},\"Status\":1},{\"Module\":{\"Id\":4,\"Name\":\"tare-weight-setting\"},\"Status\":1},{\"Module\":{\"Id\":5,\"Name\":\"incoming\"},\"Status\":1},{\"Module\":{\"Id\":6,\"Name\":\"outgoing\"},\"Status\":1},{\"Module\":{\"Id\":7,\"Name\":\"reporting\"},\"Status\":1}]", new DateTime(2023, 3, 28, 22, 5, 13, 926, DateTimeKind.Local).AddTicks(7559) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSignedIn",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "reporting-weight-list");

            migrationBuilder.InsertData(
                table: "Module",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 8, "reporting-sku-incoming" },
                    { 9, "reporting-sku-outgoing" },
                    { 10, "reporting-on-hand-balance" }
                });

            migrationBuilder.UpdateData(
                table: "TareWeightSetting",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 18, 31, 37, 775, DateTimeKind.Local).AddTicks(1444), new DateTime(2023, 2, 18, 18, 31, 37, 775, DateTimeKind.Local).AddTicks(1445) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModuleAccess", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 18, 31, 37, 773, DateTimeKind.Local).AddTicks(7493), null, new DateTime(2023, 2, 18, 18, 31, 37, 773, DateTimeKind.Local).AddTicks(7501) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 18, 31, 37, 773, DateTimeKind.Local).AddTicks(7503), new DateTime(2023, 2, 18, 18, 31, 37, 773, DateTimeKind.Local).AddTicks(7504) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 18, 31, 37, 773, DateTimeKind.Local).AddTicks(7505), new DateTime(2023, 2, 18, 18, 31, 37, 773, DateTimeKind.Local).AddTicks(7505) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 18, 31, 37, 773, DateTimeKind.Local).AddTicks(7507), new DateTime(2023, 2, 18, 18, 31, 37, 773, DateTimeKind.Local).AddTicks(7507) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsSignedIn", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 18, 18, 31, 37, 775, DateTimeKind.Local).AddTicks(1396), false, new DateTime(2023, 2, 18, 18, 31, 37, 775, DateTimeKind.Local).AddTicks(1398) });
        }
    }
}
