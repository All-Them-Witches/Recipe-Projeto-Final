using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IRecipeRepository : IRepository<int , Recipe>
    {
        //find by id , select all , add , isexist , savechange 
        Recipe FindRecipe(Expression<Func<Recipe, bool>> expression);
        List<Recipe> recipes(Expression<Func<Recipe,bool>> expression);
        public bool RemovePicture(int recipeId);
        List<Recipe> recipes();
        void Update(Recipe recipe);
        int AddRecipe(Recipe recipe);
        void AddIngredient(int recipeId,List<RecipeIngredient> recipeIngredients);
    }
}
