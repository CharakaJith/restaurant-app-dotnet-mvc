namespace restaurant_app_dotnet_mvc.Models
{
    public class ProductIngredient
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
