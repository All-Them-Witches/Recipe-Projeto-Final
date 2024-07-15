using Domain.Entities; // Importa o namespace das entidades do domínio
using Microsoft.EntityFrameworkCore; // Importa o namespace para Entity Framework Core

namespace Infrastructure // Define o namespace para a infraestrutura
{
    public class Context : DbContext // Define a classe Context que herda de DbContext
    {
        public Context(DbContextOptions<Context> options) // Construtor que recebe opções de configuração
            : base(options) // Chama o construtor da classe base DbContext
        {

        }

        public DbSet<User> users { get; set; } // Define a DbSet para a entidade User
        public DbSet<Recipe> recipes { get; set; } // Define a DbSet para a entidade Recipe
        public DbSet<RecipeIngredient> ingredients { get; set; } // Define a DbSet para a entidade RecipeIngredient
        public DbSet<UserRoles> userRoles { get; set; } // Define a DbSet para a entidade UserRoles

        protected override void OnModelCreating(ModelBuilder builder) // Método para configurar o modelo de criação do banco de dados
        {
            base.OnModelCreating(builder); // Chama o método da classe base
            var assembly = typeof(Mapping.UserMapping).Assembly; // Obtém a referência ao assembly de mapeamento
            builder.ApplyConfigurationsFromAssembly(assembly); // Aplica as configurações de mapeamento do assembly
        }
    }
}
