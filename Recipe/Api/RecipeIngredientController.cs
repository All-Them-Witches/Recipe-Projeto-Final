using Application.Contracts.RecipeIngredientContracts;
using Microsoft.AspNetCore.Mvc;

namespace Recipe.Api
{
    [Route("api/Recipe/{recipeId?}/RecipeIngredient")]
    [ApiController]
    public class RecipeIngredientController : ControllerBase
    {
        private readonly IRecipeIngredientApplication _ingredients;
        public RecipeIngredientController(IRecipeIngredientApplication ingredients)
        {
            _ingredients = ingredients;
        }

        [HttpGet]
        public IActionResult GetAll(int recipeId)
        {
            var result = _ingredients.SelectAllIngredient(recipeId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("{ingredientId}")]
        public IActionResult GetIngredient(int ingredientId)
        {
            var result = _ingredients.FindIngredient(r=>r.Id==ingredientId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete("{ingredientId}")]
        public IActionResult RemoveIngredient(int ingredientId)
        {
            var result = _ingredients.Delete(ingredientId);
            return Ok(result);
        }
    }
}
