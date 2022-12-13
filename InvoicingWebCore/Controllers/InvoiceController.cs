using InvoicingWebCore.Data;
using InvoicingWebCore.Interfaces;
using InvoicingWebCore.Models;
using InvoicingWebCore.Services;
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
        private readonly ILogger _logger;
        private readonly IInvoiceService _invoiceService;
        private readonly IUserService _userService;
        private readonly IItemService _itemService;
        private readonly IContractorService _contractorService;


        public InvoiceController(ApplicationDbContext db, IInvoiceService invoiceService, IUserService userService, IItemService itemService, ILogger<InvoiceController> logger, IContractorService contractorService)
        {
            _db = db;
            _invoiceService = invoiceService;
            _userService = userService;
            _itemService = itemService;
            _logger = logger;
            _contractorService = contractorService;
        }
        public IActionResult Index()
        {
            var invoices = _invoiceService.GetAll();            
            return View(invoices);
        }

        public IActionResult GetProducts()
        {
            var products = _itemService.GetAll();            
            return Json(products);
        }

        //get
        public IActionResult Create()
        {
            ViewBag.InvoiceTypeList = new SelectList(_invoiceService.GetInvoiceTypes(), "Id", "Name");
            ViewBag.ContractorList = new SelectList(_contractorService.GetAll(), "Id", "Name");
            ViewBag.ProductList = JsonSerializer.Serialize(_itemService.GetAll());
            ViewBag.TaxTypes = JsonSerializer.Serialize(_invoiceService.GetTaxTypes());
            ViewBag.QuantityUnits = JsonSerializer.Serialize(Invoice.QuantityUnitList);
            ViewBag.Company = _db.Companies.First();

            ViewData["InvoiceNumber"] = _invoiceService.GetInvoiceNumber();

            return View();
        }

        //post
        [HttpPost]
        public IActionResult Create(string invoice = "")
        {
            if (_invoiceService.Create(invoice))
            {
                TempData["success"] = "The invoice has been created";
               return Json(new { redirectToUrl = Url.Action("Index", "Invoice") });
            }
            else
            {
                TempData["error"] = "Sorry, an unexpected error has occurred.";             
            }
            return RedirectToAction("Index", "Invoice");
        }

        //get
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var invoice = _invoiceService.Get(id);

            if (invoice == null)
            {
                return NotFound();
            }

            ViewBag.InvoiceTypeList = new SelectList(_invoiceService.GetInvoiceTypes(), "Id", "Name");
            ViewBag.ContractorList = new SelectList(_contractorService.GetAll(), "Id", "Name");
            ViewBag.ProductList = JsonSerializer.Serialize(_itemService.GetAll());
            ViewBag.TaxTypes = JsonSerializer.Serialize(_invoiceService.GetTaxTypes());
            ViewBag.QuantityUnits = JsonSerializer.Serialize(Invoice.QuantityUnitList);
            ViewBag.Company = _db.Companies.First();

            return View(invoice);
        }

        //post
        [HttpPost]
        public IActionResult Edit(string invoiceJson = "")
        {
            var invoice = Newtonsoft.Json.JsonConvert.DeserializeObject<Invoice>(invoiceJson);

            if (_invoiceService.Update(invoice))
            {
                TempData["success"] = "The invoice has been modified";
            }
            else
            {
                TempData["error"] = "Something went wrong while modified invoice";
            }            
            return RedirectToAction("Index", "Invoice");
        }

        public IActionResult Print(int id)
        {
            var invoice = _db.Invoices
                .Include(x => x.InvoiceType)
                .Include(x => x.Products).ThenInclude(p => p.Product)
                .Include(x => x.Contractor)
                .FirstOrDefault(x => x.Id == id);

            if (invoice != null)
            {
                return View(invoice);
            }
            TempData["error"] = "Cannot find invoice";
            return RedirectToAction("Index", "Invoice");
        }

        //get
        public IActionResult Delete(int id)
        {            
            if (id == 0)
            {
                return NotFound();
            }

            if (_invoiceService.Delete(id))
            {
                TempData["success"] = "Invoice has been deleted";
            }
            else
            {
                TempData["error"] = "Invoice has not been deleted";
            }
            
            return RedirectToAction("Index");
        }
    }
}
