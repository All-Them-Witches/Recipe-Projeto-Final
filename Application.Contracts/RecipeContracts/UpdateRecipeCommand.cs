namespace Application.Contracts.RecipeContracts
{
    public class UpdateRecipeCommand : CreateRecipeCommand
    {
        public int Id { get; set; }
    }
}
