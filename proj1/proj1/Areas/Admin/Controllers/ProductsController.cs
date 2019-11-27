using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using proj1.Data;
using proj1.Models;
using proj1.Models.ViewModel;
using proj1.Utility;

namespace proj1.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironment;
        [BindProperty]
        public ProductsViewModel ProductsVM { get; set; }
        public ProductsController(ApplicationDbContext db,IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            ProductsVM = new ProductsViewModel()
            {
                Brands = _db.Brand.ToList(),
                Providers = _db.Providers.ToList(),
                ProductTypes = _db.ProductTypes.ToList(),
                Products = new Models.Products()
            };
        }
        public async Task<IActionResult> Index()
        {
            var products = _db.Products.Include(m => m.Brand).Include(m=>m.Providers).Include(m=>m.ProductTypes);
            return View(await products.ToListAsync());
        }

        //create
        public ActionResult Create()
        {
            return View(ProductsVM);
        }
        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (!ModelState.IsValid)
            {
            return View(ProductsVM);
            }
                _db.Products.Add(ProductsVM.Products);
                await _db.SaveChangesAsync();
            //image being save
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var productsFromDB = _db.Products.Find(ProductsVM.Products.ProductId);
            if (files.Count!=0)
            {
                //image has been uploaded
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(files[0].FileName);
                using (var filestream= new FileStream(Path.Combine(uploads,ProductsVM.Products.ProductId+extension),FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                productsFromDB.Image = @"\" + SD.ImageFolder + @"\" + ProductsVM.Products.ProductId + extension;
            }
            else
            {
                //when user does not upload img
                var uploads = Path.Combine(webRootPath, SD.ImageFolder+@"\"+SD.DefaultProductImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + ProductsVM.Products.ProductId + ".png");
                productsFromDB.Image = @"\" + SD.ImageFolder + @"\" + ProductsVM.Products.ProductId + ".png";
            }
            await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
        //edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductsVM.Products = await _db.Products.Include(m => m.Brand).Include(m => m.ProductTypes).Include(m => m.Providers).SingleOrDefaultAsync(m => m.ProductId == id);

            if (ProductsVM.Products == null)
            {
                return NotFound();
            }
            return View(ProductsVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var productFromDB = _db.Products.Where(m => m.ProductId == ProductsVM.Products.ProductId).FirstOrDefault();
                if (files.Count>0 && files[0]!=null)
                {
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension_new = Path.GetExtension(files[0].FileName);
                    var extension_old = Path.GetExtension(productFromDB.Image);
                    if (System.IO.File.Exists(Path.Combine(uploads,ProductsVM.Products.ProductId+extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, ProductsVM.Products.ProductId + extension_old));
                    }
                    using (var filestream = new FileStream(Path.Combine(uploads, ProductsVM.Products.ProductId + extension_new), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    ProductsVM.Products.Image = @"\" + SD.ImageFolder + @"\" + ProductsVM.Products.ProductId + extension_new;
                }
                if (ProductsVM.Products.Image!=null)
                {
                    productFromDB.Image = ProductsVM.Products.Image;
                }
                productFromDB.Name = ProductsVM.Products.Name;
                productFromDB.Price = ProductsVM.Products.Price;
                productFromDB.Quantity = ProductsVM.Products.Quantity;
                productFromDB.Description = ProductsVM.Products.Description;
                productFromDB.BrandId = ProductsVM.Products.BrandId;
                productFromDB.TypeId = ProductsVM.Products.TypeId;
                productFromDB.ProvidersId = ProductsVM.Products.ProvidersId;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ProductsVM);
        }
        //detail
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductsVM.Products = await _db.Products.Include(m => m.Brand).Include(m => m.ProductTypes).Include(m => m.Providers).SingleOrDefaultAsync(m => m.ProductId == id);

            if (ProductsVM.Products == null)
            {
                return NotFound();
            }
            return View(ProductsVM);
        }
        //delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductsVM.Products = await _db.Products.Include(m => m.Brand).Include(m => m.ProductTypes).Include(m => m.Providers).SingleOrDefaultAsync(m => m.ProductId == id);

            if (ProductsVM.Products == null)
            {
                return NotFound();
            }
            return View(ProductsVM);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            Products products = await _db.Products.FindAsync(id);
            if (products==null)
            {
                return NotFound();
            }
            else
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(products.Image);

                if (System.IO.File.Exists(Path.Combine(uploads,products.ProductId+extension)))
                {
                    System.IO.File.Delete(Path.Combine(uploads, products.ProductId + extension));
                }
                _db.Products.Remove(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        }
    }
}