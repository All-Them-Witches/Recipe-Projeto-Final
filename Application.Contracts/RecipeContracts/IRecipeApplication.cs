using Application.Contracts.RecipeIngredientContracts;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Contracts.RecipeContracts
{
    public interface IRecipeApplication
    {
        List<RecipeViewModel> SelectAllRecipes();
        public bool RemovePicture(int recipeId);
        List<RecipeViewModel> SelectAllRecipes(Expression<Func<Recipe, bool>> expression);
        RecipeViewModel FindRecipe(Expression<Func<Recipe, bool>> expression);
        bool DeleteRecipe(int RecipeId);
        bool AddRecipe(CreateRecipeCommand Recipe,out int? id);
        bool Update(UpdateRecipeCommand Recipe);
        void AddIngredients(int recipeId, List<CreateIngredientCommand> ingredientsCommand);
        void ActivateRecipe(int RecipeId);
    }
}
