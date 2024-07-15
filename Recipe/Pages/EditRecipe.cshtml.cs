using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Contracts.RecipeContracts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Application.Contracts.RecipeIngredientContracts;
using System.Security.Claims;
using Humanizer;

namespace Recipe.Pages
{
    [Authorize]
    public class EditRecipeModel : PageModel
    {
        private readonly IRecipeApplication _recipeApplication;
        public EditRecipeModel(IRecipeApplication recipeApplication)
        {
            _recipeApplication = recipeApplication;
        }
        [BindProperty]
        public UpdateRecipeCommand Recipe { get; set; } 

        public void OnGet(int recipeId)
        {
            var recipe = _recipeApplication.FindRecipe(r => r.Id == recipeId);
            Recipe = new UpdateRecipeCommand();
            #region Initializing recipe
            Recipe.Title = recipe.Title;
            Recipe.Description = recipe.Description;
            Recipe.AuthorId = recipe.AuthorId;
            Recipe.Instructions = recipe.Instructions;
            Recipe.Ingredients = recipe.Ingredients;
            Recipe.Id = recipe.Id;
            #endregion
        }
        public async Task<IActionResult> OnPost(IFormFile? recipeImage, string ingredientsStr, [FromServices] IWebHostEnvironment _hostingEnvironment)
        {
            List<CreateIngredientCommand> ingredients = new List<CreateIngredientCommand>();
            var ings = ingredientsStr.Trim().Split(',').ToList();
            //This way of saving image is strongly inefficient and unuseable and of course i don't recommend it
            //at all , but as a way to use something new i prefer to use it but ,PLEASE DON'T USE THIS WAY
            if (recipeImage != null)
            {
                var rootPath = _hostingEnvironment.WebRootPath;
                var oldPic = Path.Combine(rootPath, Recipe.Image.TrimStart('\\'));
                if (System.IO.File.Exists(oldPic))
                {
                    System.IO.File.Delete(oldPic);
                }
                var resultPic = _recipeApplication.RemovePicture(Recipe.Id);
                Recipe.Image = await CreateRecipeModel.EncodePic(recipeImage);
            }
            var result = _recipeApplication.Update(Recipe);
            foreach (var ingredient in ings)
            {
                ingredients.Add(new CreateIngredientCommand()
                {
                    IngredientName = ingredient,
                    RecipeId = Recipe.Id
                });
            }
            _recipeApplication.AddIngredients(Recipe.Id, ingredients);
            return RedirectToPage("Index");
        }
    }
}
