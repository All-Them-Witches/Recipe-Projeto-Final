using Domain.Entities;
using Domain.Repositories;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class RecipeIngredientRepository : Repository<int, RecipeIngredient>, IRecipeIngredientRepository
    {
        private Context _context;

        public RecipeIngredientRepository(Context context) : base(context)
        {
            _context = context;
        }

        public RecipeIngredient FindIngredient(Expression<Func<RecipeIngredient, bool>> expression)
        {
            return _context.ingredients.FirstOrDefault(expression);
        }

        public List<RecipeIngredient> Ingredients(Expression<Func<RecipeIngredient, bool>> expression)
        {
            return _context.ingredients.Where(expression).ToList();

        }

        public void Update(RecipeIngredient recipeIngredient)
        {
            var ingredient = Get(recipeIngredient.Id);
            ingredient.Edit(recipeIngredient.Ingredient);
            _context.SaveChanges();
        }
    }
}
