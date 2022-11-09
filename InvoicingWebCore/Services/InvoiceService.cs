using InvoicingWebCore.Data;
using InvoicingWebCore.Interfaces;
using InvoicingWebCore.Models;
using InvoicingWebCore.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace InvoicingWebCore.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _db;

        public InvoiceService(IDatabaseService databaseService, ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(string model, ApplicationUser loggedUser)
        {
            try
            {
                var invoiceModel = Newtonsoft.Json.JsonConvert.DeserializeObject<InvoiceModel>(model);                
                if (invoiceModel != null)
                {
                    Invoice invoice = new Invoice(invoiceModel);

                    invoice.Company = loggedUser.Company;
                    UpdateCompanyInvoiceNumber(loggedUser, invoice.Number);



                    _db.Invoices.Add(invoice);
                    _db.SaveChanges();

                    var updatedInvoice = _db.Invoices.FirstOrDefault(x => x.Number == invoice.Number);

                    CreateInvoiceProducts(invoiceModel.Products, updatedInvoice.Id);

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            Invoice invoice = _db.Invoices.Include(p => p.Products).FirstOrDefault(i => i.Id == id);

            if (invoice == null)
            {
                return false;
            }

            _db.Invoices.Remove(invoice);
            _db.SaveChanges();

            return true;
        }

        public bool Update(Invoice invoice, ApplicationUser loggedUser)
        {
            if (invoice != null)
            {
                string number = invoice.Number;

                Invoice inv = _db.Invoices.Find(invoice.Id);
                if (inv != null)
                {
                    inv.Company = loggedUser.Company;
                    inv.ContractorId = invoice.ContractorId;
                    inv.CreationDate = invoice.CreationDate;
                    inv.SaleDate = invoice.SaleDate;
                    inv.InvoiceTypeId = invoice.InvoiceTypeId;
                    inv.Number = number;

                    loggedUser.Company.MonthMumber = DateTime.Now.Month;
                    //user.Company.InvoiceNumberCounter = int.Parse(number.Substring(0, number.IndexOf('/')));
                    _db.Invoices.Update(inv);

                    foreach (var product in invoice.Products)
                    {
                        InvoiceProduct invoiceProduct = _db.InvoiceProducts
                            .FirstOrDefault(x => x.InvoiceId == invoice.Id && x.ProductId == product.Id);
                        if (invoiceProduct == null)
                        {
                            invoiceProduct = new()
                            {
                                InvoiceId = invoice.Id,
                                ProductId = product.Id
                            };
                        }
                        invoiceProduct.NetPrice = product.NetPrice;
                        invoiceProduct.TotalGross = product.TotalGross;
                        invoiceProduct.TotalNet = product.TotalNet;
                        invoiceProduct.Tax = product.Tax;
                        invoiceProduct.QuantityUnit = product.QuantityUnit;
                        invoiceProduct.Quantity = product.Quantity;

                        _db.InvoiceProducts.Update(invoiceProduct);
                    }
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }

        private void UpdateCompanyInvoiceNumber(ApplicationUser loggedUser, string invoiceNumber)
        {
            loggedUser.Company.MonthMumber = DateTime.Now.Month;
            loggedUser.Company.InvoiceNumberCounter = int.Parse(invoiceNumber.Substring(0, invoiceNumber.IndexOf('/')));
        }

        public void CreateInvoiceProducts(List<InvoiceProduct> invoiceProducts, int invoiceId)
        {
            foreach (var product in invoiceProducts)
            {
                InvoiceProduct invoiceProduct = new()
                {
                    InvoiceId = invoiceId,
                    ProductId = product.Id,
                    NetPrice = product.NetPrice,
                    TotalGross = product.TotalGross,
                    TotalNet = product.TotalNet,
                    Tax = product.Tax,
                    QuantityUnit = product.QuantityUnit,
                    Quantity = product.Quantity
                };
                _db.InvoiceProducts.Add(invoiceProduct);
            }
            _db.SaveChanges();
        }

        public IEnumerable<Invoice> GetAll(ApplicationUser loggedUser)
        {
            return _db.Invoices.Where(x => x.Company == loggedUser.Company).Include(it => it.InvoiceType).ToList();
        }

        public string GetInvoiceNumber(ApplicationUser loggedUser)
        {
            if (loggedUser.Company.MonthMumber < DateTime.Now.Month)
            {
                loggedUser.Company.MonthMumber = DateTime.Now.Month;
                loggedUser.Company.InvoiceNumberCounter = 1;
            }
            else
            {
                loggedUser.Company.InvoiceNumberCounter += 1;
            }

            return loggedUser.Company.InvoiceNumberCounter + "/" + loggedUser.Company.MonthMumber + "/" + DateTime.Now.Year;
        }

        public IEnumerable<InvoiceType> GetInvoiceTypes()
        {
            return _db.InvoiceTypes.ToList();
        }

        public IEnumerable<TaxType> GetTaxTypes()
        {
            return _db.TaxTypes.ToList();
        }

        public Invoice Get(int invoiceId)
        {
            return _db.Invoices
                    .Include(p => p.Products).ThenInclude(p => p.Product)
                    .Include(c => c.Company)
                    .Include(c => c.Contractor)
                    .FirstOrDefault(x => x.Id == invoiceId);
        }
    }
}