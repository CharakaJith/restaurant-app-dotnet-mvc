using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using restaurant_app_dotnet_mvc.Models;

namespace restaurant_app_dotnet_mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // define composite key and relatioins for product ingredient
            builder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductId, pi.IngredientId });

            builder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(pi => pi.ProductId);

            builder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany(i => i.ProductIngredients)
                .HasForeignKey(pi => pi.IngredientId);

            // seed data
            builder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Appetizer" },  
                new Category { CategoryId = 2, Name = "Entree" },    
                new Category { CategoryId = 3, Name = "Side Dish" },   
                new Category { CategoryId = 4, Name = "Dessert" },   
                new Category { CategoryId = 5, Name = "Beverage" }    
            );

            builder.Entity<Ingredient>().HasData(
                new Ingredient { IngredientId = 1, Name = "Beef" },    
                new Ingredient { IngredientId = 2, Name = "Chicken" },    
                new Ingredient { IngredientId = 3, Name = "Fish" },    
                new Ingredient { IngredientId = 4, Name = "Tortilla" },    
                new Ingredient { IngredientId = 5, Name = "Lettuce" },    
                new Ingredient { IngredientId = 6, Name = "Tomato" }   
            );

            builder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    ProductName = "Beef Taco",
                    ProductDescription = "A delicious beef taco",
                    Price = 3.99m,
                    Stock = 100,
                    CategoryId = 1,
                },
                new Product
                {
                    ProductId = 2,
                    ProductName = "Chicken Taco",
                    ProductDescription = "A delicious Chicken taco",
                    Price = 2.50m,
                    Stock = 200,
                    CategoryId = 2,
                },
                new Product
                {
                    ProductId = 3,
                    ProductName = "Fish Taco",
                    ProductDescription = "A delicious fish taco",
                    Price = 1.99m,
                    Stock = 75,
                    CategoryId = 3,
                }
            );

            builder.Entity<ProductIngredient>().HasData(
                new ProductIngredient { ProductId = 1, IngredientId = 1 },    
                new ProductIngredient { ProductId = 1, IngredientId = 4 },    
                new ProductIngredient { ProductId = 1, IngredientId = 5 },    
                new ProductIngredient { ProductId = 1, IngredientId = 6 },    
                new ProductIngredient { ProductId = 2, IngredientId = 2 },    
                new ProductIngredient { ProductId = 2, IngredientId = 4 },    
                new ProductIngredient { ProductId = 2, IngredientId = 5 },    
                new ProductIngredient { ProductId = 2, IngredientId = 6 },    
                new ProductIngredient { ProductId = 3, IngredientId = 3 },    
                new ProductIngredient { ProductId = 3, IngredientId = 4 }, 
                new ProductIngredient { ProductId = 3, IngredientId = 5 },  
                new ProductIngredient { ProductId = 3, IngredientId = 6 }  
            );
        }
    }
}
