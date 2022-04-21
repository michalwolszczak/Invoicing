using InvoicingWebCore.Data;
using InvoicingWebCore.Models;
using InvoicingWebCore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingWebCore.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _db;

        public InvoiceController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Invoice> objInvoiceList = _db.Invoices.ToList();
            return View(objInvoiceList);
        }

        //get
        public IActionResult Create()
        {
            InvoiceToView obj = new InvoiceToView();
            obj.Contractors = _db.Contractors.ToList();
            obj.InvoiceTypes = _db.InvoiceTypes.ToList();             
            return RedirectToAction("Select", "Contractor", obj.Contractors);
        }

        //get with contractor ID
        [HttpGet("Create/{id}")]
        public IActionResult Create([FromRoute(Name = "id")] int contractorId)
        {
            if (contractorId == null || contractorId == 0)
            {
                return NotFound();
            }

            InvoiceToView obj = new InvoiceToView();
            obj.InvoiceTypes = _db.InvoiceTypes.ToList();
            Contractor contractor = _db.Contractors.Find(contractorId);

            if (contractor == null)
            {
                return NotFound();
            }
            //obj.Contractor = contractor;

            obj.Invoice = new Invoice();
            obj.Invoice.ContractorId = contractor.Id;
            obj.Invoice.Contractor = contractor;
            obj.Invoice.SaleDate = DateTime.Now;
            obj.Invoice.CreateDate = DateTime.Now;
            return View(obj);
        }

        //post
        [HttpPost("Create/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InvoiceToView obj, [FromRoute(Name = "id")] int contractorId)
        {
            string invoiceTypeName = Request.Form["invoiceType"];
            InvoiceType objtype = _db.InvoiceTypes.Single(x => x.Name == invoiceTypeName);

            Contractor contractor = _db.Contractors.Single(x => x.Id == contractorId);

            //TODO
            obj.Invoice.Number = "1/22";
            //

            if (!ModelState.IsValid)
            {
                obj.Invoice.TypeId = objtype.Id;
                obj.Invoice.Contractor = contractor;
                obj.Invoice.ContractorId = contractor.Id;

                _db.Invoices.Add(obj.Invoice);
                _db.SaveChanges();
                TempData["success"] = "Faktura została dodana";
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
            Invoice invoice = _db.Invoices.Find(id);

            if (invoice == null)
            {
                return NotFound();
            }

            invoice.Contractor = _db.Contractors.Find(invoice.ContractorId);

            InvoiceToView obj = new InvoiceToView();
            obj.Invoice = invoice;

            InvoiceType invoiceType = _db.InvoiceTypes.Single(x => x.Id == invoice.TypeId);

            if (invoiceType == null)
            {
                return NotFound();
            }

            obj.InvoiceType = invoiceType;
            obj.InvoiceTypes = _db.InvoiceTypes.ToList();
            var productsId = _db.ProductsInvoices.Where(x => x.InvoiceId == id).ToList();
            List<Product> products = new List<Product>();

            foreach(var pro in productsId)
            {
                var product =_db.Products.Find(pro.ProductId);

                if (invoiceType != null)
                {
                    products.Add(product);
                }

            }
            obj.Products = products;

            return View(obj);
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

            return View(invoice);
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

            Invoice invoice = _db.Invoices.Find(id);

            if (invoice == null)
            {
                return NotFound();
            }

            _db.Invoices.Remove(invoice);
            _db.SaveChanges();
            TempData["success"] = "Faktura została usunięta";
            return RedirectToAction("Index");
        }

        [HttpGet("DeleteProduct/{id}/{invoiceId}")]
        public IActionResult DeleteProduct([FromRoute(Name = "id")] int? id, [FromRoute(Name = "invoiceId")] int? invoiceId)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            List<ProductInvoice> obj = _db.ProductsInvoices.Where(x => x.ProductId == id && x.InvoiceId == invoiceId).ToList();

            if (obj == null)
            {
                return NotFound();
            }

            _db.ProductsInvoices.RemoveRange(obj);
            _db.SaveChanges();
            TempData["success"] = "Produkt została usunięta z faktury";
            return RedirectToAction("Edit", new { id = invoiceId });
        }


        [HttpGet("AddProductToInvoice/{id}")]
        public IActionResult AddProductToInvoice([FromRoute(Name = "id")] int id)
        {
            List<Product> products = _db.Products.ToList();
            if(products == null || products.Count < 0)
            {
                return NotFound();                   
            }
            ProductToInvoice obj = new ProductToInvoice();
            obj.Products = products;
            obj.InvoiceId = id;

            return View(obj);               
        }

        [HttpGet("AddProductToInvoice/{id}/{invoiceId}")]
        public IActionResult AddProductToInvoice([FromRoute(Name = "id")] int id, [FromRoute(Name = "invoiceId")] int invoiceId)
        {
            if (id == 0 || invoiceId == 0)
            {
                return NotFound();
            }

            var obj = new ProductInvoice();
            obj.ProductId = id;
            obj.InvoiceId = invoiceId;

            _db.ProductsInvoices.Add(obj);
            _db.SaveChanges();

            return RedirectToAction("Edit", new { id = invoiceId });
        }       

    }
}
