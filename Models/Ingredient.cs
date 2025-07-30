namespace restaurant_app_dotnet_mvc.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}
