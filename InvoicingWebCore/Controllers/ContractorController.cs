using InvoicingWebCore.Data;
using InvoicingWebCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InvoicingWebCore.Controllers
{
    public class ContractorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ContractorController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Contractor> contractors = _db.Contractors.ToList();            
            return View(contractors);
        }
        public IActionResult Select()
        {
            IEnumerable<Contractor> contractors = _db.Contractors.ToList();
            return View(contractors);
        }

        //get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contractor obj)
        {
            if (ModelState.IsValid)
            {
                _db.Contractors.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Kontrahent został dodany";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Contractor contractor = _db.Contractors.Find(id);

            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contractor obj)
        {
            if (ModelState.IsValid)
            {
                _db.Contractors.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Kontrahent został zaktualizowany";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Contractor contractor = _db.Contractors.Find(id);

            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
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

            Contractor contractor = _db.Contractors.Find(id);

            if (contractor == null)
            {
                return NotFound();
            }

            _db.Contractors.Remove(contractor);
            _db.SaveChanges();
            TempData["success"] = "Kontrahent został usunięty";
            return RedirectToAction("Index");
        }
    }
}
