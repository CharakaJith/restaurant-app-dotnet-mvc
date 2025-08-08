using Microsoft.AspNetCore.Mvc;
using restaurant_app_dotnet_mvc.Data;
using restaurant_app_dotnet_mvc.Models;

namespace restaurant_app_dotnet_mvc.Controllers
{
    public class ProductController : Controller
    {
        private Repository<Product> products;
        private Repository<Ingredient> ingredients;
        private Repository<Category> categories;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.products = new Repository<Product>(context);
            this.ingredients = new Repository<Ingredient>(context);
            this.categories = new Repository<Category>(context);
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.products.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await this.products.GetByIdAsync(id, new QueryOption<Product>() { Includes = "ProductIngredients.Ingredient" }));
        }

        #region Product/CreateEdit/{id?}
        [HttpGet("Product/CreateEdit/{id?}")]
        public async Task<IActionResult> CreateEdit(int id)
        {
            ViewBag.Ingredients = await this.ingredients.GetAllAsync();
            ViewBag.Categories = await this.categories.GetAllAsync();

            if (id == 0)
            {
                ViewBag.Operation = "Add";
                return View(new Product());
            }
            else
            {
                ViewBag.Operation = "Edit";

                var product = await this.products.GetByIdAsync(id, new QueryOption<Product>
                {
                    Includes = "ProductIngredients.Ingredient, Category",
                    Where = p => p.ProductId == id
                });

                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
        }

        [HttpPost("Product/CreateEdit/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(Product product, int[] SelectedIngredientIds, int CategoryId)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string uploadFolder = Path.Combine(this._webHostEnvironment.WebRootPath, "images");
                    string fileName = $"{Guid.NewGuid().ToString()}_{product.ImageFile.FileName}";
                    string filePath = Path.Combine(uploadFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }
                    product.ImageUrl = fileName;
                }

                if (product.ProductId == 0)
                {
                    ViewBag.Ingredients = await this.ingredients.GetAllAsync();
                    ViewBag.Categories = await this.categories.GetAllAsync();

                    product.ProductIngredients = new List<ProductIngredient>();

                    foreach (int id in SelectedIngredientIds)
                    {
                        product.ProductIngredients.Add(new ProductIngredient
                        {
                            IngredientId = id
                        });
                    }

                    await this.products.AddAsync(product);
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    var existingProduct = await this.products.GetByIdAsync(product.ProductId, new QueryOption<Product> { Includes = "ProductIngredients" });

                    if (existingProduct == null)
                    {
                        ModelState.AddModelError("", "Product not found!");

                        ViewBag.Ingredients = await this.ingredients.GetAllAsync();
                        ViewBag.Categories = await this.categories.GetAllAsync();

                        return View(product);
                    }

                    existingProduct.ProductName = product.ProductName;
                    existingProduct.ProductDescription = product.ProductDescription;
                    existingProduct.Price = product.Price;
                    existingProduct.Stock = product.Stock;
                    existingProduct.CategoryId = CategoryId;

                    existingProduct.ProductIngredients?.Clear();
                    foreach (int id in SelectedIngredientIds)
                    {
                        existingProduct.ProductIngredients?.Add(new ProductIngredient
                        {
                            IngredientId = id,
                            ProductId = product.ProductId,
                        });
                    }

                    try
                    {
                        await this.products.UpdateAsync(existingProduct);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Error: {ex.GetBaseException().Message}");

                        ViewBag.Ingredients = await this.ingredients.GetAllAsync();
                        ViewBag.Categories = await this.categories.GetAllAsync();

                        return View(product);
                    }
                }
            }

            return RedirectToAction("Index", "Product");
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await products.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
