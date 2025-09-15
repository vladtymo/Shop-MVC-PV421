﻿using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_mvc_pv421.Data;
using Shop_mvc_pv421.Data.Entities;
using System.Reflection;
using Shop_mvc_pv421.Models;
using Shop_mvc_pv421.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

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
            // LEFT JOIN
            var model = ctx.Products.Include(x => x.Category).ToList();

            return View(model);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            SetCategoriesToViewBag();
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                SetCategoriesToViewBag();
                return View();
            }

            ctx.Products.Add(product);
            ctx.SaveChanges();

            // Passing data from controller to view:
            // 1. ViewData → dictionary(string keys) for passing data from controller to view(current request only).
            // 1. ViewBag → same as ViewData but has strong typing.
            // 3. TempData → dictionary that persists data across redirects, survives until read(next request).

            // send toast message to the view
            //TempData[WebConstants.ToastMessage] = new ToastModel("Product created successfully!");
            TempData.Set(WebConstants.ToastMessage, new ToastModel("Product created successfully!"));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = ctx.Products.Find(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ctx.Products.Update(product);
            ctx.SaveChanges();

            TempData.Set(WebConstants.ToastMessage, new ToastModel("Product updated successfully!"));

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.ADMIN)]
        public IActionResult Delete(int id)
        {
            var product = ctx.Products.Find(id);
            if (product == null) return NotFound();

            ctx.Products.Remove(product);
            ctx.SaveChanges(); // submit changes to DB

            TempData.Set(WebConstants.ToastMessage, new ToastModel("Product deleted successfully!"));

            return RedirectToAction("Index");
        }

        private void SetCategoriesToViewBag()
        {
            var categories = new SelectList(ctx.Categories.ToList(), "Id", "Name");
            ViewBag.Categories = categories;
        }
    }
}
