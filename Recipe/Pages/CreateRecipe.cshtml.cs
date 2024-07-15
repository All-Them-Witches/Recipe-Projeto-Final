using Application.Contracts.RecipeContracts; // Importa os contratos de receitas
using Application.Contracts.RecipeIngredientContracts; // Importa os contratos de ingredientes de receitas
using Application.Contracts.UserContracts; // Importa os contratos de usuários
using Microsoft.AspNetCore.Authorization; // Importa o namespace para autorização
using Microsoft.AspNetCore.Http; // Importa o namespace para manipulação de HTTP
using Microsoft.AspNetCore.Mvc; // Importa o namespace para MVC
using Microsoft.AspNetCore.Mvc.RazorPages; // Importa o namespace para Razor Pages
using System.Security.Claims; // Importa o namespace para manipulação de Claims

namespace Recipe.Pages // Define o namespace para as páginas de receitas
{
    [Authorize] // Requer autorização para acessar esta página
    public class CreateRecipeModel : PageModel // Define a classe CreateRecipeModel que herda de PageModel
    {
        [BindProperty] // Vincula a propriedade Recipe aos dados do formulário
        public CreateRecipeCommand Recipe { get; set; }
        private readonly IUserApplication _userApplication; // Campo readonly para a aplicação de usuário
        private readonly IRecipeApplication _recipeApplication; // Campo readonly para a aplicação de receitas

        public CreateRecipeModel(IUserApplication userApplication, IRecipeApplication recipeApplication) // Construtor que recebe as aplicações de usuário e receitas
        {
            _userApplication = userApplication; // Inicializa o campo _userApplication
            _recipeApplication = recipeApplication; // Inicializa o campo _recipeApplication
        }

        public void OnGet() // Método chamado em requisições GET
        {
        }

        public async Task<IActionResult> OnPost(IFormFile? recipeImage, string ingredientsStr) // Método chamado em requisições POST
        {
            List<CreateIngredientCommand> ingredients = new List<CreateIngredientCommand>(); // Inicializa a lista de ingredientes
            var ings = ingredientsStr.Trim().Split(',').ToList(); // Divide a string de ingredientes em uma lista

            // Adiciona verificações para User e Claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // Obtém o ID do usuário dos claims
            if (userIdClaim == null)
            {
                // Trata o caso onde o claim do ID do usuário está ausente
                return Unauthorized();
            }

            if (!int.TryParse(userIdClaim.Value, out int userId)) // Verifica se o ID do usuário é um inteiro válido
            {
                // Trata o caso onde o ID do usuário não é um inteiro válido
                return BadRequest("Invalid user ID.");
            }

            Recipe.AuthorId = userId; // Define o ID do autor da receita

            Recipe.Image = await EncodePic(recipeImage); // Codifica a imagem da receita

            int? recipeId; // Variável para armazenar o ID da receita
            var result = _recipeApplication.AddRecipe(Recipe, out recipeId); // Adiciona a receita e obtém o ID

            if (!recipeId.HasValue) // Verifica se o ID da receita não foi retornado
            {
                // Trata o caso onde o ID da receita não é retornado
                return BadRequest("Failed to create recipe.");
            }

            foreach (var ingredient in ings) // Itera sobre a lista de ingredientes
            {
                ingredients.Add(new CreateIngredientCommand() // Adiciona cada ingrediente à lista de comandos de criação de ingredientes
                {
                    IngredientName = ingredient,
                    RecipeId = recipeId.Value
                });
            }

            _recipeApplication.AddIngredients(recipeId.Value, ingredients); // Adiciona os ingredientes à receita
            return RedirectToPage("Index"); // Redireciona para a página inicial
        }

        public static async Task<string> EncodePic(IFormFile image) // Método estático para codificar a imagem
        {
            if (image == null) // Verifica se a imagem é nula
            {
                return null;
            }

            byte[] imageBytes; // Array de bytes para armazenar a imagem
            using (var memoryStream = new MemoryStream()) // Usa um MemoryStream para copiar a imagem
            {
                await image.CopyToAsync(memoryStream); // Copia a imagem para o MemoryStream
                imageBytes = memoryStream.ToArray(); // Converte o MemoryStream para um array de bytes
            }

            return Convert.ToBase64String(imageBytes); // Converte o array de bytes para uma string Base64 e retorna
        }
    }
}
