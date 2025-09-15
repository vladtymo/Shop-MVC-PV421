using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_mvc_pv421.Data;
using Shop_mvc_pv421.Interfaces;

namespace Shop_mvc_pv421.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ShopDbContext ctx;
        public OrdersController(ShopDbContext ctx)
        {
            this.ctx = ctx;
        }
        public IActionResult Index()
        {
            var orders = ctx.Orders.ToList();
            return View(orders);
        }
    }
}
