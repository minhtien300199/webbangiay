using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using proj1.Data;
using proj1.Models.ViewModel;
using proj1.Extensions;
using proj1.Models;
using Microsoft.EntityFrameworkCore;
namespace proj1.Areas.Customer.Controllers
{
        [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public ShopingCartModelView ShopingCartVM { get; set; }
        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;
            ShopingCartVM = new ShopingCartModelView()
            {
                Products = new List<Models.Products>()
            };
        }
        public async Task<IActionResult> Index()
        {
            List<int> lsShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lsShoppingCart == null)
            {
                return View(lsShoppingCart);
            }
            if (lsShoppingCart.Count>0)
            {
                foreach (int cartItem in lsShoppingCart)
                {
                    Products prod = _db.Products.Include(m => m.Brand).Include(m => m.ProductTypes).Where(m => m.ProductId == cartItem).FirstOrDefault();
                    ShopingCartVM.Products.Add(prod);
                }
            }
            
            return View(ShopingCartVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            List<int> lsShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            Models.Customer customer = ShopingCartVM.Customer;
            _db.Customer.Add(customer);
            _db.SaveChanges();
            int CustomerId = customer.CusId;
            for (int i = 0; i < lsShoppingCart.Count; i++)
            {
                Bill bill = new Bill()
                {
                    CusId = CustomerId,
                    ProductId = lsShoppingCart[i],
                    DatePurchase = DateTime.Now,
                    Number = ShopingCartVM.Bills[i].Number
                };
                _db.Bill.Add(bill);
            }
            _db.SaveChanges();
            lsShoppingCart = new List<int>();
            HttpContext.Session.Set("ssShoppingCart", lsShoppingCart);
            return RedirectToAction("BillConfirm", "ShoppingCart", new { id = CustomerId });
            //return RedirectToAction(nameof(Index));
        }
        //remove
        public IActionResult Remove(int id)
        {
            List<int> lsShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lsShoppingCart.Count>0)
            {
                if (lsShoppingCart.Contains(id))
                {
                    lsShoppingCart.Remove(id);
                }
            }
            HttpContext.Session.Set("ssShoppingCart", lsShoppingCart);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult BillConfirm(int id)
        {
            ShopingCartVM.Customer = _db.Customer.Where(a => a.CusId == id).FirstOrDefault();
            List<Bill> Bills = _db.Bill.Where(a => a.CusId == id).ToList();
            foreach (Bill item in Bills)
            {
                ShopingCartVM.Products.Add(_db.Products.Include(p => p.ProductTypes).Include(p => p.Providers).Include(p => p.Brand).Where(p => p.ProductId == item.ProductId).FirstOrDefault());
            }
            return View(ShopingCartVM);
        }
    }
}