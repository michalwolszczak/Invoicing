using InvoicingWebCore.Data;
using InvoicingWebCore.Models;
using InvoicingWebCore.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace InvoicingWebCore.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _db;

        public InvoiceController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _db.Users.Include(c => c.Company).FirstOrDefault(x => x.Id == userId);

            IEnumerable<Invoice> objInvoiceList = _db.Invoices.Where(x => x.Company == user.Company).ToList();
            return View(objInvoiceList);
        }

        public IActionResult GetProducts()
        {
            var products = _db.Products.ToList<Product>;
            var test = Json(products);
            return Json(products);           
        }

        //get
        public IActionResult Create()
        {
            //logges user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _db.Users.Include(c => c.Company).FirstOrDefault( x=> x.Id == userId);

            if(user != null)
            {             
                if(user.Company != null)
                {
                    ViewBag.InvoiceTypeList = new SelectList(_db.InvoiceTypes.ToList(), "Id", "Name");
                    ViewBag.ContractorList = new SelectList(_db.Contractors.ToList(), "Id", "Name");
                    ViewBag.ProductList = JsonSerializer.Serialize(_db.Products.ToList());
                    ViewBag.TaxTypes = JsonSerializer.Serialize(_db.TaxTypes.ToList());
                    ViewBag.Company = user.Company;

                    InvoiceNumber invoiceNumber = new();

                    if (user.Company.MonthMumber < DateTime.Now.Month)
                    {
                        user.Company.MonthMumber = DateTime.Now.Month;
                        user.Company.InvoiceNumberCounter = 1;
                    }
                    else
                    {
                        user.Company.InvoiceNumberCounter += 1;
                    }
                    invoiceNumber.SetNumber(user.Company.InvoiceNumberCounter);
                    ViewData["InvoiceNumber"] = invoiceNumber.Build();
                }
                return View();
            }

            TempData["error"] = "The user in not logged in";
            return RedirectToAction("Login","Account");
        }

        [HttpPost]
        public JsonResult UpdateContractor(int contractorId)
        {
            var contractor = _db.Contractors.Find(contractorId);
            if(contractor != null)
            {
                //InvoiceNumber invoiceNumber = new InvoiceNumber();

                //if (company.MonthMumber < DateTime.Now.Month)
                //{
                //    company.MonthMumber = DateTime.Now.Month;
                //    company.InvoiceNumberCounter = 1;
                //}
                //else
                //{
                //    company.InvoiceNumberCounter += 1;
                //}
                //invoiceNumber.SetNumber(company.InvoiceNumberCounter);
                return Json(contractor);
            }
            return Json(null);
        }
        //post
        [HttpPost]
        public JsonResult Create(string invoice = "")
        {            
            dynamic test = Newtonsoft.Json.JsonConvert.DeserializeObject(invoice);            
            var a = test.invoiceTypeId;
            //string invoiceTypeName = Request.Form["invoiceType"];
            //InvoiceType objtype = _db.InvoiceTypes.Single(x => x.Name == invoiceTypeName);

            //Contractor contractor = _db.Contractors.Single(x => x.Id == model.ContractorId);

            ////TODO
            //model.Number = "1/22";
            ////

            //if (!ModelState.IsValid)
            //{
            //    model.InvoiceTypeId = objtype.Id;
            //    model.Contractor = contractor;
            //    model.ContractorId = contractor.Id;

            //    _db.Invoices.Add(model);
            //    _db.SaveChanges();
            //    TempData["success"] = "Faktura została dodana";
            //    return RedirectToAction("Index");
            //}
            var t = "";
            return Json(invoice);
        }

        //get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Invoice invoice = _db.Invoices.Include(p => p.Products).FirstOrDefault(x => x.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _db.Invoices.Update(invoice);
                _db.SaveChanges();
                TempData["success"] = "Faktura została zaktualizowana";
                return RedirectToAction("Index");
            }
            return View(invoice);
        }
        //get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Invoice invoice = _db.Invoices.Find(id);

            if (invoice == null)
            {
                return NotFound();
            }
            _db.Invoices.Remove(invoice);
            _db.SaveChanges();
            TempData["success"] = "Invoice has been deleted";
            return RedirectToAction("Index");
        }      
    }
}
