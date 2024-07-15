using Application.Contracts.RecipeContracts; // Importa os contratos de receitas
using Application.Contracts.RecipeIngredientContracts; // Importa os contratos de ingredientes de receitas
using Application.Contracts.UserContracts; // Importa os contratos de usu�rios
using Microsoft.AspNetCore.Authorization; // Importa o namespace para autoriza��o
using Microsoft.AspNetCore.Http; // Importa o namespace para manipula��o de HTTP
using Microsoft.AspNetCore.Mvc; // Importa o namespace para MVC
using Microsoft.AspNetCore.Mvc.RazorPages; // Importa o namespace para Razor Pages
using System.Security.Claims; // Importa o namespace para manipula��o de Claims

namespace Recipe.Pages // Define o namespace para as p�ginas de receitas
{
    [Authorize] // Requer autoriza��o para acessar esta p�gina
    public class CreateRecipeModel : PageModel // Define a classe CreateRecipeModel que herda de PageModel
    {
        [BindProperty] // Vincula a propriedade Recipe aos dados do formul�rio
        public CreateRecipeCommand Recipe { get; set; }
        private readonly IUserApplication _userApplication; // Campo readonly para a aplica��o de usu�rio
        private readonly IRecipeApplication _recipeApplication; // Campo readonly para a aplica��o de receitas

        public CreateRecipeModel(IUserApplication userApplication, IRecipeApplication recipeApplication) // Construtor que recebe as aplica��es de usu�rio e receitas
        {
            _userApplication = userApplication; // Inicializa o campo _userApplication
            _recipeApplication = recipeApplication; // Inicializa o campo _recipeApplication
        }

        public void OnGet() // M�todo chamado em requisi��es GET
        {
        }

        public async Task<IActionResult> OnPost(IFormFile? recipeImage, string ingredientsStr) // M�todo chamado em requisi��es POST
        {
            List<CreateIngredientCommand> ingredients = new List<CreateIngredientCommand>(); // Inicializa a lista de ingredientes
            var ings = ingredientsStr.Trim().Split(',').ToList(); // Divide a string de ingredientes em uma lista

            // Adiciona verifica��es para User e Claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // Obt�m o ID do usu�rio dos claims
            if (userIdClaim == null)
            {
                // Trata o caso onde o claim do ID do usu�rio est� ausente
                return Unauthorized();
            }

            if (!int.TryParse(userIdClaim.Value, out int userId)) // Verifica se o ID do usu�rio � um inteiro v�lido
            {
                // Trata o caso onde o ID do usu�rio n�o � um inteiro v�lido
                return BadRequest("Invalid user ID.");
            }

            Recipe.AuthorId = userId; // Define o ID do autor da receita

            Recipe.Image = await EncodePic(recipeImage); // Codifica a imagem da receita

            int? recipeId; // Vari�vel para armazenar o ID da receita
            var result = _recipeApplication.AddRecipe(Recipe, out recipeId); // Adiciona a receita e obt�m o ID

            if (!recipeId.HasValue) // Verifica se o ID da receita n�o foi retornado
            {
                // Trata o caso onde o ID da receita n�o � retornado
                return BadRequest("Failed to create recipe.");
            }

            foreach (var ingredient in ings) // Itera sobre a lista de ingredientes
            {
                ingredients.Add(new CreateIngredientCommand() // Adiciona cada ingrediente � lista de comandos de cria��o de ingredientes
                {
                    IngredientName = ingredient,
                    RecipeId = recipeId.Value
                });
            }

            _recipeApplication.AddIngredients(recipeId.Value, ingredients); // Adiciona os ingredientes � receita
            return RedirectToPage("Index"); // Redireciona para a p�gina inicial
        }

        public static async Task<string> EncodePic(IFormFile image) // M�todo est�tico para codificar a imagem
        {
            if (image == null) // Verifica se a imagem � nula
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
