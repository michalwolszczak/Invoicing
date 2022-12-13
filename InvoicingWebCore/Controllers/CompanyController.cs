using InvoicingWebCore.Data;
using InvoicingWebCore.Models;
using InvoicingWebCore.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIP24;
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

                _db.Companies.Add(company);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _db.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return NotFound();
            }
            var company = _db.Companies.First();

            return View(company);
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

        [HttpPost]
        public IActionResult GetCompanyData(string nip = "")
        {
            if(nip != "")
            {
                NIP24Client nip24 = new NIP24Client("uKM4ARSA8hPg", "UeyaO517V4Mz");
                AllData all = nip24.GetAllData(Number.NIP, nip);

                if(nip24.LastError != "")
                {
                    Company company = new()
                    {
                        Name = all.Name,
                        FirstName = all.FirstName,
                        LastName = all.LastName,
                        Regon = all.REGON,
                        AddressLine1 = all.StreetNumber + " " + all.Street + ", " + all.HouseNumber,
                        City = all.City,
                        PostalCode = all.PostCode,
                        Province = all.State,
                        Country = all.County
                    };

                    return Json(company);
                }
            }       
            return Json("");
        }
    }
}
