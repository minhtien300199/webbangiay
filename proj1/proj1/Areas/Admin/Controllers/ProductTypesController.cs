using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using proj1.Data;
using proj1.Models;
using proj1.Utility;

namespace proj1.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            
            return View(_db.ProductTypes.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Add(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            }
            return View(productTypes);
        }
        //get action edit
        public async Task<IActionResult> Edit(int ? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var producType = await _db.ProductTypes.FindAsync(id);
            if (producType==null)
            {
                return NotFound();
            }
            return View(producType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,ProductTypes productTypes)
        {
            if (id != productTypes.TypeId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }
        //details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producType = await _db.ProductTypes.FindAsync(id);
            if (producType == null)
            {
                return NotFound();
            }
            return View(producType);
        }
        //delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producType = await _db.ProductTypes.FindAsync(id);
            if (producType == null)
            {
                return NotFound();
            }
            return View(producType);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, ProductTypes productTypes)
        {
            var producTypes = await _db.ProductTypes.FindAsync(id);
            _db.ProductTypes.Remove(productTypes);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }
    }
}