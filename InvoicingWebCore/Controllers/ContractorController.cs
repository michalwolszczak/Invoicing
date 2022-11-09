using InvoicingWebCore.Data;
using InvoicingWebCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _db.Users.Include(c => c.Company).FirstOrDefault(x => x.Id == userId);

            IEnumerable<Contractor> contractors = _db.Contractors.Where(x => x.Company == user.Company).ToList();            
            return View(contractors);
        }

        //get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contractor contractor)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _db.Users.Include(c => c.Company).FirstOrDefault(x => x.Id == userId);
            if(user != null)
            {
                ModelState.Remove("Company");
                ModelState.Remove("CompanyId");
                if (ModelState.IsValid)
                {
                    contractor.Company = user.Company;
                    _db.Contractors.Add(contractor);
                    _db.SaveChanges();

                    TempData["success"] = "A new contractor has been added";
                    return RedirectToAction("Index");
                }
                TempData["error"] = "Something went wrong";
                return View(contractor);
            }
            TempData["error"] = "The user in not logged in";
            return RedirectToAction("Login", "Account");
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
            ModelState.Remove("Company");
            if (ModelState.IsValid)
            {
                _db.Contractors.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Contractor has been updated";
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

            _db.Contractors.Remove(contractor);
            _db.SaveChanges();
            TempData["success"] = "Contractor has been deleted";
            return RedirectToAction("Index");
        }
    }
}
