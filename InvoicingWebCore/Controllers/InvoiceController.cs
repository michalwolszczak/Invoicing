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
        private ApplicationUser _loggedUser;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);            
            _loggedUser = _userService.GetLoggedUser(userId);

            var invoices = _invoiceService.GetAll(_loggedUser);            
            return View(invoices);
        }

        public IActionResult GetProducts()
        {
            var products = _itemService.GetAll(_loggedUser);            
            return Json(products);
        }

        //get
        public IActionResult Create()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggedUser = _userService.GetLoggedUser(userId);

                if(_loggedUser != null)
                {
                    ViewBag.InvoiceTypeList = new SelectList(_invoiceService.GetInvoiceTypes(), "Id", "Name");
                    ViewBag.ContractorList = new SelectList(_contractorService.GetAll(_loggedUser), "Id", "Name");
                    ViewBag.ProductList = JsonSerializer.Serialize(_itemService.GetAll(_loggedUser));
                    ViewBag.TaxTypes = JsonSerializer.Serialize(_invoiceService.GetTaxTypes());
                    ViewBag.QuantityUnits = JsonSerializer.Serialize(Invoice.QuantityUnitList);
                    ViewBag.Company = _loggedUser.Company;

                    ViewData["InvoiceNumber"] = _invoiceService.GetInvoiceNumber(_loggedUser);
                    
                    return View();
                }
                
                TempData["error"] = "The user in not logged in";
                return RedirectToAction("Login", "Account");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult UpdateContractor(int contractorId)
        {
            var contractor = _contractorService.Update(contractorId);
            return Json(contractor);
        }

        //post
        [HttpPost]
        public IActionResult Create(string invoice = "")
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggedUser = _userService.GetLoggedUser(userId);

                if (_loggedUser != null)
                {                                        
                    if(_invoiceService.Create(invoice, _loggedUser))
                    {
                        TempData["success"] = "The invoice has been created";
                        return Json(new { redirectToUrl = Url.Action("Index", "Invoice") });
                    }
                    else
                    {
                        TempData["error"] = "Sorry, an unexpected error has occurred.";
                        return RedirectToAction("Index", "Invoice");
                    }
                }
                TempData["error"] = "The user in not logged in";
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot create invoice");
                throw;
            }
        }

        //get
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _loggedUser = _userService.GetLoggedUser(userId);

            if (_loggedUser != null)
            {
                ViewBag.InvoiceTypeList = new SelectList(_invoiceService.GetInvoiceTypes(), "Id", "Name");
                ViewBag.ContractorList = new SelectList(_contractorService.GetAll(_loggedUser), "Id", "Name");
                ViewBag.ProductList = JsonSerializer.Serialize(_itemService.GetAll(_loggedUser));
                ViewBag.TaxTypes = JsonSerializer.Serialize(_invoiceService.GetTaxTypes());
                ViewBag.QuantityUnits = JsonSerializer.Serialize(Invoice.QuantityUnitList);

                var invoice = _invoiceService.Get(id);

                if (invoice == null)
                {
                    return NotFound();
                }

                return View(invoice);
            }
            TempData["error"] = "Raczej nie możliwe, że to się stało";
            return NotFound();
        }

        //post
        [HttpPost]        
        public IActionResult Edit(string invoiceJson = "")
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggedUser = _userService.GetLoggedUser(userId);

                if (_loggedUser != null)
                {
                    var invoice = Newtonsoft.Json.JsonConvert.DeserializeObject<Invoice>(invoiceJson);

                    if (_invoiceService.Update(invoice, _loggedUser))
                    {
                        TempData["success"] = "The invoice has been modified";
                        return Json(new { redirectToUrl = Url.Action("Index", "Invoice") });
                    }

                    TempData["error"] = "Cannot read the json file";
                    return NotFound();
                }
                TempData["error"] = "The user in not logged in";
                return RedirectToAction("Login", "Account");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult Print(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _db.Users.Include(c => c.Company).FirstOrDefault(x => x.Id == userId);

            if(user != null)
            {
                var invoice = _db.Invoices
                    .Include(x => x.InvoiceType)
                    .Include(x => x.Products).ThenInclude(p => p.Product)
                    .Include(x => x.Company)
                    .Include(x => x.Contractor)
                    .FirstOrDefault(x => x.Id == id);
                
                if(invoice != null)
                {
                    return View(invoice);
                }
                TempData["error"] = "Cannot find invoice";
                return RedirectToAction("Index", "Invoice");
            }
            TempData["error"] = "The user in not logged in";
            return RedirectToAction("Login", "Account");            
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
                return RedirectToAction("Index");
            }

            TempData["error"] = "Invoice has not been deleted";
            return RedirectToAction("Index");
        }
    }
}
