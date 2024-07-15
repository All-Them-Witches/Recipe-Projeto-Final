using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RecipeRatingremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ratings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 1, 21, 26, 53, 234, DateTimeKind.Local).AddTicks(3949),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 31, 15, 59, 38, 334, DateTimeKind.Local).AddTicks(6373));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 1, 21, 26, 53, 233, DateTimeKind.Local).AddTicks(6105),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 31, 15, 59, 38, 333, DateTimeKind.Local).AddTicks(8506));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ingredient_add_date",
                table: "ingredients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 1, 21, 26, 53, 233, DateTimeKind.Local).AddTicks(1099),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 31, 15, 59, 38, 333, DateTimeKind.Local).AddTicks(6087));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1000,
                column: "RegisterDate",
                value: new DateTime(2023, 9, 1, 21, 26, 53, 234, DateTimeKind.Local).AddTicks(3949));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 31, 15, 59, 38, 334, DateTimeKind.Local).AddTicks(6373),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 1, 21, 26, 53, 234, DateTimeKind.Local).AddTicks(3949));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 31, 15, 59, 38, 333, DateTimeKind.Local).AddTicks(8506),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 1, 21, 26, 53, 233, DateTimeKind.Local).AddTicks(6105));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ingredient_add_date",
                table: "ingredients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 31, 15, 59, 38, 333, DateTimeKind.Local).AddTicks(6087),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 1, 21, 26, 53, 233, DateTimeKind.Local).AddTicks(1099));

            migrationBuilder.CreateTable(
                name: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsRemoved = table.Column<string>(type: "nvarchar(8)", nullable: false),
                    recipe_rate = table.Column<byte>(type: "tinyint", nullable: false),
                    rate_confirmed_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 8, 31, 15, 59, 38, 334, DateTimeKind.Local).AddTicks(2285))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ratings_recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ratings_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1000,
                column: "RegisterDate",
                value: new DateTime(2023, 8, 31, 15, 59, 38, 334, DateTimeKind.Local).AddTicks(6373));

            migrationBuilder.CreateIndex(
                name: "IX_ratings_RecipeId",
                table: "ratings",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_UserId",
                table: "ratings",
                column: "UserId");
        }
    }
}
