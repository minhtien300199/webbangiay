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
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BrandController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Brand.ToList());
        }
        //create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _db.Add(brand);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }
        //edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var brand = await _db.Brand.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Brand brand)
        {
            if (id != brand.BrandID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(brand);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }
        //delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var brand = await _db.Brand.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Brand brand)
        {
            var brands = await _db.Brand.FindAsync(id);
            _db.Brand.Remove(brand);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}