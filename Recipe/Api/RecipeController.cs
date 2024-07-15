using Application; 
using Application.Contracts.RecipeContracts; 
using Application.Contracts.UserContracts; 
using Microsoft.AspNetCore.Mvc; 

namespace Recipe.Api // Define o namespace para a API de receitas
{
    [Route("api/[controller]")] // Define a rota para o controlador
    [ApiController] // Indica que esta classe é um controlador de API
    public class RecipeController : ControllerBase // Define a classe RecipeController que herda de ControllerBase
    {
        private readonly IRecipeApplication _recipeApplication; // Campo readonly para a aplicação de receitas

        public RecipeController(IRecipeApplication recipeApplication) // Construtor que recebe a aplicação de receitas
        {
            _recipeApplication = recipeApplication; // Inicializa o campo _recipeApplication
        }

        // GET: api/<RecipeController>
        [HttpGet] // Define o método HTTP GET
        public IActionResult Get() // Método para obter todas as receitas
        {
            var result = _recipeApplication.SelectAllRecipes(); // Seleciona todas as receitas
            return Ok(result); // Retorna as receitas com status 200 OK
        }

        // GET api/<RecipeController>/5
        [HttpGet("{id}")] // Define o método HTTP GET com um parâmetro id
        public IActionResult Get(int id) // Método para obter uma receita pelo id
        {
            var result = _recipeApplication.FindRecipe(r => r.Id == id); // Encontra a receita pelo id
            if (result != null)
            {
                return Ok(result); // Retorna a receita com status 200 OK
            }
            return NotFound(); // Retorna status 404 Not Found se a receita não for encontrada
        }

        // POST api/<RecipeController>
        [HttpPost] // Define o método HTTP POST
        public IActionResult Post([FromBody] CreateRecipeCommand recipeCmd) // Método para criar uma nova receita
        {
            int? id;
            var result = _recipeApplication.AddRecipe(recipeCmd, out id); // Adiciona a nova receita
            if (result)
            {
                var url = Url.Action(nameof(Get), "Recipe", new { id = id }, Request.Scheme); // Gera a URL para a nova receita
                return Created(url, recipeCmd); // Retorna a nova receita com status 201 Created
            }
            return BadRequest(); // Retorna status 400 Bad Request se a criação falhar
        }

        // PUT api/<RecipeController>/5
        [HttpPut("{id}")] // Define o método HTTP PUT com um parâmetro id
        public IActionResult Put([FromBody] UpdateRecipeCommand recipeCmd) // Método para atualizar uma receita
        {
            var isSuccesed = _recipeApplication.Update(recipeCmd); // Atualiza a receita
            // Regras RESTful devem ser seguidas
            // Se o objeto não existir em uma solicitação PUT, ele deve ser criado
            if (!isSuccesed)
            {
                var createcmd = new CreateRecipeCommand
                {
                    AuthorId = recipeCmd.AuthorId,
                    Description = recipeCmd.Description,
                    Ingredients = recipeCmd.Ingredients,
                    Instructions = recipeCmd.Instructions,
                    Title = recipeCmd.Title,
                    Image = recipeCmd.Image,
                };
                int? newid;
                isSuccesed = _recipeApplication.AddRecipe(createcmd, out newid); // Cria a nova receita se a atualização falhar
            }
            if (isSuccesed)
                return Ok(); // Retorna status 200 OK se a criação ou atualização for bem-sucedida

            return BadRequest(); // Retorna status 400 Bad Request se a criação ou atualização falhar
        }

        // DELETE api/<RecipeController>/5
        [HttpDelete("{id}")] // Define o método HTTP DELETE com um parâmetro id
        public IActionResult Delete(int id) // Método para deletar uma receita
        {
            var result = _recipeApplication.DeleteRecipe(id); // Deleta a receita pelo id
            if (result)
                return Ok(); // Retorna status 200 OK se a exclusão for bem-sucedida

            return NotFound(); // Retorna status 404 Not Found se a receita não for encontrada
        }
    }
}
