namespace restaurant_app_dotnet_mvc.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int Stock {  get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<ProductIngredient>? ProductIngredients { get; set; }
    }
}
