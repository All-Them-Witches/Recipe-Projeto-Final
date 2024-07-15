namespace Application.Contracts.RecipeIngredientContracts
{
    public class UpdateIngredientCommand : CreateIngredientCommand
    {
        public int Id { get; set; }
    }
}
