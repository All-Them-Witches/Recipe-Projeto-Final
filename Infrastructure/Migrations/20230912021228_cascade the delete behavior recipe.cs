using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cascadethedeletebehaviorrecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ingredients_recipes_RecipeId",
                table: "ingredients");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 11, 19, 12, 28, 305, DateTimeKind.Local).AddTicks(7018),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 6, 15, 27, 24, 143, DateTimeKind.Local).AddTicks(4070));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 11, 19, 12, 28, 305, DateTimeKind.Local).AddTicks(2703),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 6, 15, 27, 24, 142, DateTimeKind.Local).AddTicks(9905));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ingredient_add_date",
                table: "ingredients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 11, 19, 12, 28, 304, DateTimeKind.Local).AddTicks(9866),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 6, 15, 27, 24, 142, DateTimeKind.Local).AddTicks(7092));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1000,
                column: "RegisterDate",
                value: new DateTime(2023, 9, 11, 19, 12, 28, 305, DateTimeKind.Local).AddTicks(7018));

            migrationBuilder.AddForeignKey(
                name: "FK_ingredients_recipes_RecipeId",
                table: "ingredients",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ingredients_recipes_RecipeId",
                table: "ingredients");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 6, 15, 27, 24, 143, DateTimeKind.Local).AddTicks(4070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 11, 19, 12, 28, 305, DateTimeKind.Local).AddTicks(7018));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 6, 15, 27, 24, 142, DateTimeKind.Local).AddTicks(9905),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 11, 19, 12, 28, 305, DateTimeKind.Local).AddTicks(2703));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ingredient_add_date",
                table: "ingredients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 6, 15, 27, 24, 142, DateTimeKind.Local).AddTicks(7092),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 11, 19, 12, 28, 304, DateTimeKind.Local).AddTicks(9866));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1000,
                column: "RegisterDate",
                value: new DateTime(2023, 9, 6, 15, 27, 24, 143, DateTimeKind.Local).AddTicks(4070));

            migrationBuilder.AddForeignKey(
                name: "FK_ingredients_recipes_RecipeId",
                table: "ingredients",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
