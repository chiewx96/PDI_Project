using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDI_Feather_Tracking_API.Migrations
{
    /// <inheritdoc />
    public partial class AddModuleSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                  table: "Module",
                  columns: new[] { "Id","Name" },
                  values: new object[,]
                  {
                    { 1, "user-level" },
                    { 2, "user" },
                    { 3, "sku-type" },
                    { 4, "tare-weight-setting" },
                    { 5, "incoming" },
                    { 6, "outgoing" },
                    { 7, "reporting-weight-list" },
                    { 8, "reporting-sku-incoming" },
                    { 9, "reporting-sku-outgoing" },
                    { 10, "reporting-on-hand-balance" }
                  });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new object[1, 2, 3, 4, 5]);


        }
    }
}
