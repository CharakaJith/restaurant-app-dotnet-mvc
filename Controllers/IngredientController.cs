using Microsoft.AspNetCore.Mvc;
using restaurant_app_dotnet_mvc.Data;
using restaurant_app_dotnet_mvc.Models;

namespace restaurant_app_dotnet_mvc.Controllers
{
    public class IngredientController : Controller
    {
        private Repository<Ingredient> ingredients;

        public IngredientController(ApplicationDbContext context)
        {
            this.ingredients = new Repository<Ingredient>(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.ingredients.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await this.ingredients.GetByIdAsync(id, new QueryOption<Ingredient>() { Includes = "ProductIngredients.Product" }));
        }

        #region Ingredient/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IngredientId, Name")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await this.ingredients.AddAsync(ingredient);

                return RedirectToAction("Index");
            }

            return View(ingredient);
        }
        #endregion

        #region Ingredient/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await this.ingredients.GetByIdAsync(id, new QueryOption<Ingredient> { Includes = "ProductIngredients.Product" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Ingredient ingredient)
        {
            await this.ingredients.DeleteAsync(ingredient.IngredientId);

            return RedirectToAction("Index");
        }
        #endregion

        #region Ingredient/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await this.ingredients.GetByIdAsync(id, new QueryOption<Ingredient> { Includes = "ProductIngredients.Product" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await this.ingredients.UpdateAsync(ingredient);

                return RedirectToAction("Index");
            }

            return View(ingredient);
        }
        #endregion
    }
}
