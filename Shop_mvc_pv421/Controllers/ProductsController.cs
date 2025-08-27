﻿using Microsoft.AspNetCore.Mvc;
using Shop_mvc_pv421.Data;

namespace Shop_mvc_pv421.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopDbContext ctx;

        public ProductsController(ShopDbContext ctx)
        {
            this.ctx = ctx;
        }
        public IActionResult Index()
        {
            var model = ctx.Products.ToList();

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var product = ctx.Products.Find(id);
            if (product == null) return NotFound();

            ctx.Products.Remove(product);
            ctx.SaveChanges(); // submit changes to DB

            return RedirectToAction("Index");
        }
    }
}
