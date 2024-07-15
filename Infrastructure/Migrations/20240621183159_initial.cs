using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 21, 19, 31, 59, 187, DateTimeKind.Local).AddTicks(3527),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 11, 19, 12, 28, 305, DateTimeKind.Local).AddTicks(7018));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 21, 19, 31, 59, 186, DateTimeKind.Local).AddTicks(5379),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 11, 19, 12, 28, 305, DateTimeKind.Local).AddTicks(2703));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ingredient_add_date",
                table: "ingredients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 21, 19, 31, 59, 185, DateTimeKind.Local).AddTicks(8054),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 11, 19, 12, 28, 304, DateTimeKind.Local).AddTicks(9866));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1000,
                column: "RegisterDate",
                value: new DateTime(2024, 6, 21, 19, 31, 59, 187, DateTimeKind.Local).AddTicks(3527));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 11, 19, 12, 28, 305, DateTimeKind.Local).AddTicks(7018),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 21, 19, 31, 59, 187, DateTimeKind.Local).AddTicks(3527));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 11, 19, 12, 28, 305, DateTimeKind.Local).AddTicks(2703),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 21, 19, 31, 59, 186, DateTimeKind.Local).AddTicks(5379));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ingredient_add_date",
                table: "ingredients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 11, 19, 12, 28, 304, DateTimeKind.Local).AddTicks(9866),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 21, 19, 31, 59, 185, DateTimeKind.Local).AddTicks(8054));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1000,
                column: "RegisterDate",
                value: new DateTime(2023, 9, 11, 19, 12, 28, 305, DateTimeKind.Local).AddTicks(7018));
        }
    }
}
