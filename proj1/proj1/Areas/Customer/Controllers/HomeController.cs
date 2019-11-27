﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using proj1.Models;
using proj1.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using proj1.Extensions;

namespace proj1.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        //public HomeController(ApplicationDbContext db)
        //{
        //    _db = db;
        //}
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var productsList =_db.Products.Include(m => m.ProductTypes).Include(m => m.Brand).Include(m=>m.Providers);
            return View(await productsList.ToListAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            var products = await _db.Products.Include(m => m.ProductTypes).Include(m => m.Brand).Include(m => m.Providers).Where(m=>m.ProductId==id).FirstOrDefaultAsync();
            return View(products);
        }
        [HttpPost,ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsPost(int id)
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstShoppingCart==null)
            {
                lstShoppingCart = new List<int>();
            }
            lstShoppingCart.Add(id);
            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);
            return RedirectToAction("Index", "Home",new { area="Customer"});
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Remove(int id)
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstShoppingCart.Count>0)
            {
                if (lstShoppingCart.Contains(id))
                {
                    lstShoppingCart.Remove(id);

                }
            }
            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);
            return RedirectToAction(nameof(Index));
        }
    }
}
