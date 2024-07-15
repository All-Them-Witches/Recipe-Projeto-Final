using Application.Contracts.RecipeContracts;
using Application.Contracts.UserContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Recipe.Pages
{
    public class RecipeDetailModel : PageModel
    {
        [BindProperty]
        public RecipeViewModel recipe { get; set; }
        private readonly IRecipeApplication _recipeApplication;
        private readonly IUserApplication _userApplication;

        public RecipeDetailModel(IRecipeApplication recipeApplication, IUserApplication userApplication)
        {
            _recipeApplication = recipeApplication;
            _userApplication = userApplication;
        }
        public IActionResult OnGet(int recipeId)
        {
            var Recipe = _recipeApplication.FindRecipe(t => t.Id == recipeId);
            if (Recipe != null)
                recipe = Recipe;
            else
               return NotFound();

            return Page();
        }
    }
}
