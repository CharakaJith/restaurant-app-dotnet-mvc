using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using restaurant_app_dotnet_mvc.Data;
using restaurant_app_dotnet_mvc.Models;

namespace restaurant_app_dotnet_mvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private Repository<Product> products;
        private Repository<Order> orders;
        
        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> user)
        {
            this._context = context;
            this._userManager = user;
            this.products = new Repository<Product>(context);
            this.orders = new Repository<Order>(context);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await this.products.GetAllAsync()
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddItem(int prodId, int prodQty)
        {
            var product = await this._context.Products.FindAsync(prodId);
            if (product == null)
            {
                return NotFound();
            }

            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products= await this.products.GetAllAsync()
            };

            var existingItem = model.OrderItems.FirstOrDefault(i => i.ProductId == prodId);
            if (existingItem != null)
            {
                existingItem.Quantity += prodQty;
            }
            else
            {
                model.OrderItems.Add(new OrderItemViewModel
                {
                    ProductId = product.ProductId,
                    Price = product.Price,
                    Quantity = prodQty,
                    ProductName = product.ProductName,
                });
            }

            model.TotalAmount = model.OrderItems.Sum(i => i.Price * i.Quantity);

            HttpContext.Session.Set("OrderViewModel", model);

            return RedirectToAction("Create", model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");
            if (model == null || model.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");
            if (model == null || model.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }

            Order order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = model.TotalAmount,
                UserId = _userManager.GetUserId(User)
            };

            foreach (var item in model.OrderItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                });
            }

            await this.orders.AddAsync(order);

            HttpContext.Session.Remove("OrderViewModel");

            return RedirectToAction("ViewOrders");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ViewOrders()
        {
            var userId = _userManager.GetUserId(User);

            var userOrders = await this.orders.GetAllByIdAsync(userId, "UserId", new QueryOption<Order>
            {
                Includes = "OrderItems.Product"
            });

            return View(userOrders);
        }
    }
}
