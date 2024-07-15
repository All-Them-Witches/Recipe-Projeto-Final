using Application.Contracts.RecipeIngredientContracts;
using System.Text.Json.Serialization;

namespace Application.Contracts.RecipeContracts
{
    public class CreateRecipeCommand
    {
        public string Title { get;  set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        [JsonIgnore]
        public string Image { get; set; }
        [JsonIgnore]
        public List<CreateIngredientCommand> Ingredients { get; set; }
        public int AuthorId { get; set; }
    }
}
