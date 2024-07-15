using System; 
using Microsoft.EntityFrameworkCore.Migrations; 

#nullable disable // Desativa as anotações de nulidade

namespace Infrastructure.Migrations // Define o namespace da migração
{
    /// <inheritdoc />
    public partial class addingthedatabase : Migration // Define a classe de migração parcial
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Método para aplicar as mudanças no banco de dados
        {
            migrationBuilder.CreateTable( // Cria a tabela "users"
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false) // Define a coluna Id como chave primária
                        .Annotation("SqlServer:Identity", "1000, 1"), // Define a identidade da coluna
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false), // Define a coluna Username
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false), // Define a coluna Email
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false), // Define a coluna Password
                    IsActive = table.Column<string>(type: "nvarchar(8)", nullable: false), // Define a coluna IsActive
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 8, 31, 15, 59, 38, 334, DateTimeKind.Local).AddTicks(6373)) // Define a coluna RegisterDate
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id); // Define a chave primária da tabela
                });

            migrationBuilder.CreateTable( // Cria a tabela "recipes"
                name: "recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false) // Define a coluna Id como chave primária
                        .Annotation("SqlServer:Identity", "1, 1"), // Define a identidade da coluna
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false), // Define a coluna Title
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "This is the best recipe that you can find"), // Define a coluna Description
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false), // Define a coluna Instructions
                    IsRemoved = table.Column<string>(type: "nvarchar(10)", nullable: false), // Define a coluna IsRemoved
                    AuthorId = table.Column<int>(type: "int", nullable: false), // Define a coluna AuthorId
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 8, 31, 15, 59, 38, 333, DateTimeKind.Local).AddTicks(8506)) // Define a coluna RegisterDate
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipes", x => x.Id); // Define a chave primária da tabela
                    table.ForeignKey( // Define a chave estrangeira para a tabela "users"
                        name: "FK_recipes_users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade); // Define o comportamento de exclusão em cascata
                });

            migrationBuilder.CreateTable( // Cria a tabela "ingredients"
                name: "ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false) // Define a coluna Id como chave primária
                        .Annotation("SqlServer:Identity", "1, 1"), // Define a identidade da coluna
                    IngredientName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false), // Define a coluna IngredientName
                    Quantity = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false), // Define a coluna Quantity
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false), // Define a coluna IsRemoved
                    RecipeId = table.Column<int>(type: "int", nullable: false), // Define a coluna RecipeId
                    ingredient_add_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 8, 31, 15, 59, 38, 333, DateTimeKind.Local).AddTicks(6087)) // Define a coluna ingredient_add_date
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredients", x => x.Id); // Define a chave primária da tabela
                    table.ForeignKey( // Define a chave estrangeira para a tabela "recipes"
                        name: "FK_ingredients_recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // Define o comportamento de exclusão restrito
                });

            migrationBuilder.CreateTable( // Cria a tabela "ratings"
                name: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false) // Define a coluna Id como chave primária
                        .Annotation("SqlServer:Identity", "1, 1"), // Define a identidade da coluna
                    recipe_rate = table.Column<byte>(type: "tinyint", nullable: false), // Define a coluna recipe_rate
                    IsRemoved = table.Column<string>(type: "nvarchar(8)", nullable: false), // Define a coluna IsRemoved
                    UserId = table.Column<int>(type: "int", nullable: false), // Define a coluna UserId
                    RecipeId = table.Column<int>(type: "int", nullable: false), // Define a coluna RecipeId
                    rate_confirmed_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 8, 31, 15, 59, 38, 334, DateTimeKind.Local).AddTicks(2285)) // Define a coluna rate_confirmed_date
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratings", x => x.Id); // Define a chave primária da tabela
                    table.ForeignKey( // Define a chave estrangeira para a tabela "recipes"
                        name: "FK_ratings_recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // Define o comportamento de exclusão restrito
                    table.ForeignKey( // Define uma chave estrangeira para a tabela "users"
                        name: "FK_ratings_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // Define o comportamento de exclusão restrito
                });

            migrationBuilder.InsertData( // Insere dados na tabela "users"
                table: "users",
                columns: new[] { "Id", "Email", "IsActive", "Password", "Username" },
                values: new object[] { 1000, "ZGVmQGRlZmF1bHQuZGVmYXVsdA==", "Active", "default123", "default user" });

            migrationBuilder.CreateIndex( // Cria um índice na coluna RecipeId da tabela "ingredients"
                name: "IX_ingredients_RecipeId",
                table: "ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex( // Cria um índice na coluna RecipeId da tabela "ratings"
                name: "IX_ratings_RecipeId",
                table: "ratings",
                column: "RecipeId");

            migrationBuilder.CreateIndex( // Cria um índice na coluna UserId da tabela "ratings"
                name: "IX_ratings_UserId",
                table: "ratings",
                column: "UserId");

            migrationBuilder.CreateIndex( // Cria um índice na coluna AuthorId da tabela "recipes"
                name: "IX_recipes_AuthorId",
                table: "recipes",
                column: "AuthorId");

            migrationBuilder.CreateIndex( // Cria um índice único na coluna Username da tabela "users"
                name: "IX_users_Username",
                table: "users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Método para reverter as mudanças no banco de dados
        {
            migrationBuilder.DropTable( // Remove a tabela "ingredients"
                name: "ingredients");

            migrationBuilder.DropTable( // Remove a tabela "ratings"
                name: "ratings");

            migrationBuilder.DropTable( // Remove a tabela "recipes"
                name: "recipes");

            migrationBuilder.DropTable( // Remove a tabela "users"
                name: "users");
        }
    }
}
