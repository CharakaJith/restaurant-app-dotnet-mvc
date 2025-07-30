using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace restaurant_app_dotnet_mvc.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }

        [ValidateNever]
        public ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}
