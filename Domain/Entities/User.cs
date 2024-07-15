using System.Text.Json.Serialization; // Importa o namespace para a serialização JSON

namespace Domain.Entities // Define o namespace para as entidades do domínio
{
    public class User // Define a classe User
    {
        public int Id { get; private set; } // Propriedade Id com acesso privado de escrita
        public string Username { get; private set; } // Propriedade Username com acesso privado de escrita
        public string Email { get; private set; } // Propriedade Email com acesso privado de escrita
        public string Password { get; private set; } // Propriedade Password com acesso privado de escrita
        public bool IsActive { get; private set; } // Propriedade IsActive com acesso privado de escrita

        public ICollection<Recipe> _createdRecipes { get; set; } // Coleção de receitas criadas com acesso público de leitura e escrita
        public ICollection<UserRoles> roles { get; private set; } // Coleção de papéis do usuário com acesso privado de escrita
        private User() { } // Construtor privado para impedir a criação sem parâmetros
        public User(string username, string email, string password, int id = 0) // Construtor público com parâmetros
        {
            this.Username = username; // Inicializa o nome de usuário
            this.Email = email; // Inicializa o email
            this.Password = password; // Inicializa a senha
            this.IsActive = true; // Define o usuário como ativo por padrão
            this.roles = new List<UserRoles>(); // Inicializa a lista de papéis
            if (id > 0)
                this.Id = id; // Define o Id se for maior que 0
        }
        public void Edit(string username, string email) // Método para editar o nome de usuário e email
        {
            Username = username; // Atualiza o nome de usuário
            Email = email; // Atualiza o email
        }
        public void ChangePassword(string newPasswrod) // Método para mudar a senha
        {
            this.Password = newPasswrod; // Atualiza a senha
        }
        public void DeActive() // Método para desativar o usuário
        {
            IsActive = false; // Define o usuário como inativo
        }
        public void Activate() // Método para ativar o usuário
        {
            IsActive = true; // Define o usuário como ativo
        }

        public void AssignRole(UserRoles role) // Método para atribuir um papel ao usuário
        {
            this.roles.Add(role); // Adiciona o papel à lista de papéis
        }

        public Recipe CreateRecipe(string title, string description, string instructions, string image) // Método para criar uma nova receita
        {
            var recipe = new Recipe(title, description, instructions, image, this.Id); // Cria uma nova receita
            _createdRecipes.Add(recipe); // Adiciona a receita à lista de receitas criadas
            return recipe; // Retorna a receita criada
        }
        public IReadOnlyCollection<Recipe> GetCreatedRecipes() // Método para obter as receitas criadas
        {
            return _createdRecipes.ToList().AsReadOnly(); // Retorna uma coleção somente leitura das receitas criadas
        }
    }

}
