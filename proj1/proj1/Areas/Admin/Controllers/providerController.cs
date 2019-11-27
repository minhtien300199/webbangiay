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
    public class providerController : Controller
    {
        private readonly ApplicationDbContext _db;
        public providerController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Providers.ToList());
        }
        //create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Providers providers)
        {
            if (ModelState.IsValid)
            {
                _db.Add(providers);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(providers);
        }
        //edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var providers = await _db.Providers.FindAsync(id);
            if (providers == null)
            {
                return NotFound();
            }
            return View(providers);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Providers providers)
        {
            if (id != providers.ProviderID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(providers);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(providers);
        }
        //delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var providers = await _db.Providers.FindAsync(id);
            if (providers == null)
            {
                return NotFound();
            }
            return View(providers);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Providers providers)
        {
            var providers1 = await _db.Providers.FindAsync(id);
            _db.Providers.Remove(providers1);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}