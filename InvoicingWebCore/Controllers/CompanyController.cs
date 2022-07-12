using InvoicingWebCore.Data;
using InvoicingWebCore.Models;
using InvoicingWebCore.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InvoicingWebCore.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CompanyController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var companies = _db.Companies.ToList();
            return View(companies);
        }        

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _db.Users.Find(userId);

            if (company != null && user != null)
            {
                company.MonthMumber = DateTime.Now.Month;
                company.InvoiceNumberCounter = 0;
                company.User = user;

                _db.Companies.Add(company);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _db.Users.Include(c => c.Company).FirstOrDefault(x => x.Id == userId);

            if (user.Company == null)
            {
                return NotFound();
            }

            return View(user.Company);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                _db.Companies.Update(company);
                _db.SaveChanges();
                TempData["success"] = "Company information has been updated";
                return RedirectToAction("Index", "Home");
            }
            return View(company);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Company company = _db.Companies.Find(id);

            if (company == null)
            {
                return NotFound();
            }

            _db.Companies.Remove(company);
            _db.SaveChanges();
            TempData["success"] = "Company has been deleted";
            return RedirectToAction("Index");
        }
    }
}
