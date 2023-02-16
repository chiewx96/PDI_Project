using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace PDIFeatherTrackingAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserLevelModuleAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLevels_ModuleAccess_ModuleAccessid",
                table: "UserLevels");

            migrationBuilder.DropTable(
                name: "ModuleAccess");

            migrationBuilder.DropIndex(
                name: "IX_UserLevels_ModuleAccessid",
                table: "UserLevels");

            migrationBuilder.DropColumn(
                name: "ModuleAccessid",
                table: "UserLevels");

            migrationBuilder.AddColumn<string>(
                name: "ModuleAccess",
                table: "UserLevels",
                type: "longtext",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TareWeightSetting",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 17, 1, 20, 32, 654, DateTimeKind.Local).AddTicks(722), new DateTime(2023, 2, 17, 1, 20, 32, 654, DateTimeKind.Local).AddTicks(723) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModuleAccess", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 17, 1, 20, 32, 614, DateTimeKind.Local).AddTicks(7751), null, new DateTime(2023, 2, 17, 1, 20, 32, 614, DateTimeKind.Local).AddTicks(7763) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModuleAccess", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 17, 1, 20, 32, 614, DateTimeKind.Local).AddTicks(7766), null, new DateTime(2023, 2, 17, 1, 20, 32, 614, DateTimeKind.Local).AddTicks(7766) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModuleAccess", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 17, 1, 20, 32, 614, DateTimeKind.Local).AddTicks(7767), null, new DateTime(2023, 2, 17, 1, 20, 32, 614, DateTimeKind.Local).AddTicks(7768) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModuleAccess", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 17, 1, 20, 32, 614, DateTimeKind.Local).AddTicks(7769), null, new DateTime(2023, 2, 17, 1, 20, 32, 614, DateTimeKind.Local).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 17, 1, 20, 32, 654, DateTimeKind.Local).AddTicks(341), "jXpIBK5Y/okx6e3tDfKTIASl6ciNv+W1xSAfnj7VGDZThIIb", new DateTime(2023, 2, 17, 1, 20, 32, 654, DateTimeKind.Local).AddTicks(353) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModuleAccess",
                table: "UserLevels");

            migrationBuilder.AddColumn<int>(
                name: "ModuleAccessid",
                table: "UserLevels",
                type: "int",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "TareWeightSetting",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 15, 0, 34, 30, 529, DateTimeKind.Local).AddTicks(3084), new DateTime(2023, 2, 15, 0, 34, 30, 529, DateTimeKind.Local).AddTicks(3084) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModuleAccessid", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7925), null, new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7932) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModuleAccessid", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7934), null, new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7935) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModuleAccessid", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7936), null, new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7937) });

            migrationBuilder.UpdateData(
                table: "UserLevels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModuleAccessid", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7938), null, new DateTime(2023, 2, 15, 0, 34, 30, 489, DateTimeKind.Local).AddTicks(7939) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 2, 15, 0, 34, 30, 529, DateTimeKind.Local).AddTicks(2796), "Q/iS86IdHUgC3V8frPyd61GB+jqnhwtyE8AI9ijFed37IFyX", new DateTime(2023, 2, 15, 0, 34, 30, 529, DateTimeKind.Local).AddTicks(2810) });

            migrationBuilder.CreateIndex(
                name: "IX_UserLevels_ModuleAccessid",
                table: "UserLevels",
                column: "ModuleAccessid");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLevels_ModuleAccess_ModuleAccessid",
                table: "UserLevels",
                column: "ModuleAccessid",
                principalTable: "ModuleAccess",
                principalColumn: "id");
        }
    }
}
