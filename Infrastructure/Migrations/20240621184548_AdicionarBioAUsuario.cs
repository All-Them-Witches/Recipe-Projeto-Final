using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarBioAUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 21, 19, 45, 47, 885, DateTimeKind.Local).AddTicks(9304),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 21, 19, 31, 59, 187, DateTimeKind.Local).AddTicks(3527));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 21, 19, 45, 47, 883, DateTimeKind.Local).AddTicks(4688),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 21, 19, 31, 59, 186, DateTimeKind.Local).AddTicks(5379));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ingredient_add_date",
                table: "ingredients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 21, 19, 45, 47, 880, DateTimeKind.Local).AddTicks(1946),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 21, 19, 31, 59, 185, DateTimeKind.Local).AddTicks(8054));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1000,
                column: "RegisterDate",
                value: new DateTime(2024, 6, 21, 19, 45, 47, 885, DateTimeKind.Local).AddTicks(9304));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 21, 19, 31, 59, 187, DateTimeKind.Local).AddTicks(3527),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 21, 19, 45, 47, 885, DateTimeKind.Local).AddTicks(9304));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 21, 19, 31, 59, 186, DateTimeKind.Local).AddTicks(5379),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 21, 19, 45, 47, 883, DateTimeKind.Local).AddTicks(4688));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ingredient_add_date",
                table: "ingredients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 21, 19, 31, 59, 185, DateTimeKind.Local).AddTicks(8054),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 21, 19, 45, 47, 880, DateTimeKind.Local).AddTicks(1946));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1000,
                column: "RegisterDate",
                value: new DateTime(2024, 6, 21, 19, 31, 59, 187, DateTimeKind.Local).AddTicks(3527));
        }
    }
}
