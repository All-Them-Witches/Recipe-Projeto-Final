namespace Domain.Entities // Define o namespace para as entidades do domínio
{
    public class Recipe // Define a classe Recipe
    {
        public int Id { get; private set; } // Propriedade Id com acesso privafo de leitura e escrita
        public string Title { get; set; } // Propriedade Title com acesso público de leitura e escrita
        public string Description { get; set; } // Propriedade Description com acesso público de leitura e escrita
        public string Instructions { get; set; } // Propriedade Instructions com acesso público de leitura e escrita
        public bool IsRemoved { get; set; } // Propriedade IsRemoved com acesso público de leitura e escrita
        public int AuthorId { get; private set; } // Propriedade AuthorId com acesso privado de leitura e escrita
        public string Image { get; set; } // Propriedade Image com acesso público de leitura e escrita
        public User Author { get; set; } // Propriedade Author com acesso público de leitura e escrita

        public ICollection<RecipeIngredient> _ingredients { get; set; } // Coleção de ingredientes com acesso público de leitura e escrita
        private Recipe() { } // Construtor privado para impedir a criação sem parâmetros
        public Recipe(string title, string description, string instructions, string image, int authorId, int id = 0) // Construtor público com parâmetros
        {
            Title = title;
            Description = description;
            Instructions = instructions;
            IsRemoved = false;
            AuthorId = authorId;
            _ingredients = new List<RecipeIngredient>(); // Inicializa a lista de ingredientes
            this.Image = image;
            if (id != 0)
                this.Id = id; // Define o Id se for diferente de 0
        }
        public void EditIngrediant(List<RecipeIngredient> recipeIngredients) // Método para editar os ingredientes
        {
            this._ingredients = recipeIngredients;
        }
        public void Delete() // Método para marcar a receita como removida
        {
            IsRemoved = true;
        }
        public void RemovePicture() // Método para remover a imagem da receita
        {
            this.Image = "";
        }
        public void Restore() // Método para restaurar a receita
        {
            IsRemoved = true;
        }
        public void AddIngredient(RecipeIngredient ingredient) // Método para adicionar um ingrediente à receita
        {
            _ingredients.Add(ingredient);
        }
        public void Update(Recipe recipe) // Método para atualizar a receita
        {
            this.Title = recipe.Title;
            this.Description = recipe.Description;
            this.Instructions = recipe.Instructions;
        }

        public List<RecipeIngredient> FetchIngredients() // Método para buscar os ingredientes da receita
        {
            return (List<RecipeIngredient>)this._ingredients;
        }
    }
}
