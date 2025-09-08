using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_mvc_pv421.Data;
using Shop_mvc_pv421.Extensions;

namespace Shop_mvc_pv421.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopDbContext ctx;

        public CartController(ShopDbContext ctx)
        {
            this.ctx = ctx;
        }
        // GET: Cart
        public ActionResult Index()
        {
            var existingIds = HttpContext.Session.Get<List<int>>("CartItems") ?? new List<int>();

            var items = ctx.Products
                .Include(x => x.Category)
                .Where(x => existingIds.Contains(x.Id))
                .ToList();
            
            return View(items);
        }
        
        public ActionResult Add(int id) // 3, 5
        {
            var existingIds = HttpContext.Session.Get<List<int>>("CartItems");
            List<int> ids = existingIds ?? new();
            
            ids.Add(id);

            HttpContext.Session.Set("CartItems", ids);
            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Remove()
        {
            return View();
        }
    }
}
