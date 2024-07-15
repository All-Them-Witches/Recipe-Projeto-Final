namespace Domain.Entities
{
    public class RecipeIngredient
    {
        public int Id { get; private set; }
        public string Ingredient { get; private set; }
        public bool IsRemoved { get; private set; }
        public int RecipeId { get; private set; }
        public Recipe? Recipe { get; set; }
        private RecipeIngredient()
        {

        }
        public RecipeIngredient(string ingredientName, int recipeId, int id = 0)
        {
            this.Ingredient = ingredientName;
            RecipeId = recipeId;
            IsRemoved = false;
            if (id > 0)
                this.Id = id;
        }
        public void Remove()
        {
            this.IsRemoved = true;
        }
        public void Restore()
        {
            this.IsRemoved = false;
        }
        public void Edit(string name)
        {
            this.Ingredient = name;
        }
    }
}
