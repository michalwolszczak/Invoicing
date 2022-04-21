using InvoicingWebCore.Data;
using InvoicingWebCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingWebCore.Controllers
{
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
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                TempData["success"] = "Produkt został utworzony";
                return RedirectToAction("Index");
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

            return View(product);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
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

            return View(product);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
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
            TempData["success"] = "Produkt został usunięty";
            return RedirectToAction("Index");
        }
    }
}
