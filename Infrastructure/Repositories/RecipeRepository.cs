using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class RecipeRepository : Repository<int, Recipe>, IRecipeRepository
    {
        private Context _context;

        public RecipeRepository(Context context)
            : base(context)
        {
            _context = context;
        }

        public void AddIngredient(int recipeId, List<RecipeIngredient> ingredients)
        {
            var recipe = _context.recipes.Find(recipeId);
            if (recipe != null)
            {
                foreach (var ingredient in ingredients)
                {
                    recipe.AddIngredient(ingredient);
                }
                _context.SaveChanges();
            }
        }

        public int AddRecipe(Recipe recipe)
        {
            _context.recipes.Add(recipe);
            _context.SaveChanges();
            return recipe.Id;
        }

        public Recipe FindRecipe(Expression<Func<Recipe, bool>> expression)
        {
            return _context.recipes.Include(r => r._ingredients).FirstOrDefault(expression);
        }

        public List<Recipe> recipes(Expression<Func<Recipe, bool>> expression)
        {
            return _context.recipes.Include(i => i._ingredients).Where(expression).ToList();
        }

        public List<Recipe> recipes()
        {
            return _context.recipes.Include(r => r._ingredients).ToList();
        }

        public bool RemovePicture(int recipeId)
        {
            var result = _context.recipes.Find(recipeId);
            if (result != null)
            {
                result.RemovePicture();
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Update(Recipe recipe)
        {
            var Recipe = Get(recipe.Id);
            Recipe.Update(recipe);
            _context.SaveChanges();
        }
    }
}
