using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ingredientsquantityremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngredientName",
                table: "ingredients");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ingredients",
                newName: "Ingredient");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 6, 15, 27, 24, 143, DateTimeKind.Local).AddTicks(4070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 4, 12, 41, 48, 662, DateTimeKind.Local).AddTicks(4918));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 6, 15, 27, 24, 142, DateTimeKind.Local).AddTicks(9905),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 4, 12, 41, 48, 661, DateTimeKind.Local).AddTicks(8106));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ingredient_add_date",
                table: "ingredients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 6, 15, 27, 24, 142, DateTimeKind.Local).AddTicks(7092),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 4, 12, 41, 48, 661, DateTimeKind.Local).AddTicks(3801));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1000,
                column: "RegisterDate",
                value: new DateTime(2023, 9, 6, 15, 27, 24, 143, DateTimeKind.Local).AddTicks(4070));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "recipes");

            migrationBuilder.RenameColumn(
                name: "Ingredient",
                table: "ingredients",
                newName: "Quantity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 4, 12, 41, 48, 662, DateTimeKind.Local).AddTicks(4918),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 6, 15, 27, 24, 143, DateTimeKind.Local).AddTicks(4070));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 4, 12, 41, 48, 661, DateTimeKind.Local).AddTicks(8106),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 6, 15, 27, 24, 142, DateTimeKind.Local).AddTicks(9905));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ingredient_add_date",
                table: "ingredients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 4, 12, 41, 48, 661, DateTimeKind.Local).AddTicks(3801),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 6, 15, 27, 24, 142, DateTimeKind.Local).AddTicks(7092));

            migrationBuilder.AddColumn<string>(
                name: "IngredientName",
                table: "ingredients",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1000,
                column: "RegisterDate",
                value: new DateTime(2023, 9, 4, 12, 41, 48, 662, DateTimeKind.Local).AddTicks(4918));
        }
    }
}
