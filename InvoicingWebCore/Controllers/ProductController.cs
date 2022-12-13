using InvoicingWebCore.Data;
using InvoicingWebCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InvoicingWebCore.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Products.ToList();
            return View(products);
        }

        //get
        public IActionResult Create()
        {
            ViewBag.TaxTypes = new SelectList(_db.TaxTypes.ToList(), "Tax", "Tax");
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _db.Users.FirstOrDefault(x => x.Id == userId);

            if(user != null)
            {
                ModelState.Remove("Company");
                ModelState.Remove("Invoices");
                if (ModelState.IsValid)
                {
                    _db.Products.Add(product);
                    _db.SaveChanges();
                    TempData["success"] = "A new product has been added";
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }

        //get
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Product product = _db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.TaxTypes = new SelectList(_db.TaxTypes.ToList(), "Tax", "Tax");
            return View(product);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            ModelState.Remove("Company");
            if (ModelState.IsValid)
            {
                _db.Products.Update(product);
                _db.SaveChanges();
                TempData["success"] = "Produkt został zaktualizowany";
                return RedirectToAction("Index");
            }
            return View(product);
        }
        //get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            _db.SaveChanges();
            TempData["success"] = "Product has been deleted";
            return RedirectToAction("Index");
        }
    }
}
