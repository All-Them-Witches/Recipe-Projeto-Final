using Application.Contracts.RecipeIngredientContracts;
using Domain.Entities;
using Domain.Repositories;
using System.Linq.Expressions;

namespace Application
{
    public class RecipeIngredientApplication : IRecipeIngredientApplication
    {
        private IRecipeIngredientRepository _ingradientRepository;

        public RecipeIngredientApplication(IRecipeIngredientRepository ingradientRepository)
        {
            _ingradientRepository = ingradientRepository; 
        }

        public void ActivateIngredient(int IngredientId)
        {
            var ingredient = _ingradientRepository.Get(IngredientId);
            if (ingredient != null)
            {
                ingredient.Restore();
                _ingradientRepository.SaveChanges();
            }
        }

        public void AddIngredient(CreateIngredientCommand createIngredientcommand)
        {
            var ingredient = new RecipeIngredient(createIngredientcommand.IngredientName, createIngredientcommand.RecipeId);
            _ingradientRepository.Create(ingredient);
            _ingradientRepository.SaveChanges();
        }

        public bool Delete(int IngredientId)
        {
            var ingredient = _ingradientRepository.Get(IngredientId);
            if (ingredient != null)
            {
                ingredient.Remove();
                _ingradientRepository.SaveChanges();
                return true;
            }
            return false;
        }

        public IngredientViewModel FindIngredient(Expression<Func<RecipeIngredient, bool>> expression)
        {
            var recipe = _ingradientRepository.FindIngredient(expression);
            return new IngredientViewModel
            {
                RecipeId = recipe.RecipeId,
                Id = recipe.Id,
                IngredientName = recipe.Ingredient,
            };
        }

        public List<IngredientViewModel> SelectAllIngredient()
        {
            return _ingradientRepository.Get().Select(t=> new IngredientViewModel
            {
                Id = t.Id,
                IngredientName =t.Ingredient,
                RecipeId = t.RecipeId,
            }).OrderByDescending(t => t.Id).ToList();
        }

        public List<IngredientViewModel> SelectAllIngredient(int recipeId)
        {
            var result = _ingradientRepository.Ingredients(r=>r.RecipeId==recipeId).Select(t=>
            new IngredientViewModel
            {
                RecipeId = t.Id,
                IngredientName =t.Ingredient,
                Id = t.RecipeId,
            }).ToList();
            return result;
        }

        public void Update(UpdateIngredientCommand ingredient)
        {
            var recipeUpdate = _ingradientRepository.Get(ingredient.Id);
            if (recipeUpdate != null)
            {

                var updateIngredient = new RecipeIngredient(ingredient.IngredientName,ingredient.Id);
                _ingradientRepository.Update(updateIngredient);
            }
            else
                throw new NullReferenceException();
        }
    }
}
